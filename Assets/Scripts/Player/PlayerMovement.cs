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
    private float rotationSpeed = 20f;
    private float rotationSmooth = 5f; 
    private CharacterController controller;
    private Vector3 velocity;
    private Animator anim;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        HandleMovement(horizontalInput, verticalInput);
        HandleRotation(horizontalInput);
       
    }

    void HandleMovement(float horizontalInput, float verticalInput)
    {

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Convertir movimiento local a global según la dirección del personaje
        Vector3 moveDirection = transform.TransformDirection(movement);

        // Aplicar movimiento con CharacterController
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        // Aplicar gravedad
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        // Pasar valores al Animator
        anim.SetFloat("SpeedZ", verticalInput);
        anim.SetFloat("SpeedX", -1 * horizontalInput);
    }


void HandleRotation(float horizontalInput)
{
    if (Mathf.Abs(horizontalInput) > 0.1f)
    {
        float targetAngle = transform.eulerAngles.y + (horizontalInput * rotationSpeed);
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmooth);
    }
}

}