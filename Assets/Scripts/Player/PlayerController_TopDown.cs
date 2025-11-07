using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController_TopDown : MonoBehaviour
{

    [Header("Movement Settings")]
    [Tooltip("Velocidad de movimiento del jugador.")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] LayerMask groundLayer;
    // [SerializeField] Transform target;
    [SerializeField] float rotationSpeed = 5f;

    private Rigidbody rb;
    private Camera mainCamera;
    //private float lastFireTime;
    //MiraController miraController;
    //private ObjectPooler objectPooler;
    private Animator anim;
    // private float speed=0f;
    private void Awake()
    {
        //objectPooler = ObjectPooler.Instance;
    }
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();

        mainCamera = Camera.main;

        //miraController = target.GetComponent<MiraController>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody no encontrado");
        }
        if (mainCamera == null)
        {
            Debug.LogError("Cámara principal no encontrada");
        }
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
        //HandleAnimation();
        //HandleFireInput();
        HandleFlashlight();
    }

    private void HandleFlashlight()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            // flashLight.SetActive(!flashLight.activeInHierarchy);
        }
    }

    void HandleMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        rb.velocity = movement * moveSpeed + new Vector3(0f, rb.velocity.y, 0f); // Conserva la velocidad vertical

        // Calcular velocidad real (en unidades por segundo)
        float currentSpeed = rb.velocity.magnitude;

        // Pasar valores al Animator
        // anim.SetFloat("SpeedZ", verticalInput);
        // anim.SetFloat("SpeedX", horizontalInput);



    }

    void HandleRotation()
    {
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(cameraRay, out hit, Mathf.Infinity, groundLayer))
        {
            Vector3 pointToLook = hit.point;
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.red);

            // Dirección hacia el punto de impacto, ignorando la altura
            Vector3 direction = (pointToLook - transform.position).normalized;
            direction.y = 0f;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }
}
