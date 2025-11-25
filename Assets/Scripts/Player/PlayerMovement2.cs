using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 6f;
    public float gravity = -9.81f;
    public float rotationSpeed = 10f;

    [Header("Referencias")]
    public Transform cameraTransform; // ← ASIGNAR MANUALMENTE

    private CharacterController controller;
    private Vector3 velocity;
    private Animator anim;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        
        // Si no se asignó manualmente, usar Camera.main
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
            Debug.Log("Usando Camera.main como fallback");
        }
    }

    void Update()
    {
        if(GameManager.Instance.isGameOver) return;
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        HandleMovementAndRotation(horizontalInput, verticalInput);
    }

    void HandleMovementAndRotation(float horizontalInput, float verticalInput)
    {
        Vector3 inputDirection = new Vector3(horizontalInput, 0, verticalInput);

        if (inputDirection.magnitude > 0.1f)
        {
            // Usar la cámara asignada
            Vector3 cameraForward = cameraTransform.forward;
            Vector3 cameraRight = cameraTransform.right;
            
            cameraForward.y = 0;
            cameraRight.y = 0;
            cameraForward.Normalize();
            cameraRight.Normalize();

            Vector3 moveDirection = (cameraForward * verticalInput + cameraRight * horizontalInput).normalized;

            controller.Move(moveDirection * moveSpeed * Time.deltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            anim.SetFloat("SpeedZ", verticalInput);
            anim.SetFloat("SpeedX", horizontalInput);
        }
        else
        {
            anim.SetFloat("SpeedZ", 0);
            anim.SetFloat("SpeedX", 0);
        }

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
