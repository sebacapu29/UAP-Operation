using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 6f;
    public float gravity = -9.81f;

    [Header("Rotación")]
    public float mouseSensitivity = 100f;
    public Transform playerBody; 
    public Transform cameraTransform;
    private float rotationSpeed = 90f;
    private float rotationSmooth = 5f; 
    private CharacterController controller;
    private Vector3 velocity;
    private Animator anim;
    
    private float currentMouseX;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
       
    }

    void HandleMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Convertir movimiento local a global según la dirección del personaje
        Vector3 moveDirection = transform.TransformDirection(movement);

        // Aplicar movimiento con CharacterController
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        // Aplicar gravedad
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        // HandleRotation(horizontalInput);
        // Pasar valores al Animator
        anim.SetFloat("SpeedZ", verticalInput);
        anim.SetFloat("SpeedX", -1 * horizontalInput);
    }


 void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        currentMouseX += mouseX;

        // Rotación suave del Player
        Quaternion targetRotation = Quaternion.Euler(0f, currentMouseX, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmooth * Time.deltaTime);
    }

}