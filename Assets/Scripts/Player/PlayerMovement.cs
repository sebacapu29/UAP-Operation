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

    private CharacterController controller;
    private Vector3 velocity;
    // private float xRotation = 0f;
    private Actions actions;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        actions = GetComponentInChildren<Actions>();
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = cameraTransform.forward * vertical + cameraTransform.right * horizontal;
        moveDirection.y = 0f; 

        controller.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        bool isMoving = moveDirection.magnitude > 0;
        if (isMoving)
            actions.Run();
        else
            actions.Stay();
    }

    void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        Vector3 lookDirection = new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z);
        if (lookDirection != Vector3.zero)
        {
            playerBody.rotation = Quaternion.LookRotation(lookDirection);
        }
    }
}