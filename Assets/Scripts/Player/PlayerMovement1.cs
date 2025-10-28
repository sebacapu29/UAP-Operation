using UnityEngine;

// Requiere que el GameObject tenga un componente CharacterController.
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement1 : MonoBehaviour
{
    [Header("Player Movement")]
    
    [SerializeField] float moveSpeed = 6f;                    // Velocidad de movimiento del jugador.
    
    [SerializeField] float mouseSensitivity = 100f;           // Sensibilidad del ratón para rotar la cámara.
    
    [SerializeField] Transform playerCamera;                  // Referencia a la cámara del jugador para la rotación.

  //-----------------------------------------------------------------------------------------------------------
    [Header("Projectile System")]
    
    [SerializeField] string projectileTag = "Bullet";         // El tiempo de espera entre disparos.
    
    [SerializeField] float fireCooldown = 0.5f;               // Punto de spawn desde donde se lanzarán los proyectiles.
    
    [SerializeField] Transform firePoint;

    //------------------------------------------------------------------------------------------------------------
    // Referencias a los componentes.
    private CharacterController controller;
    private ObjectPooler objectPooler;

    // Variables internas para la lógica de disparo y rotación.
    private float lastFireTime;
    private float rotationX = 0f;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        objectPooler = ObjectPooler.Instance;
    }

    void Update()
    {
        HandlePlayerMovement();
        HandleMouseLook();
        HandleFireInput();
    }

    private void HandlePlayerMovement()
    {
        // Obtener la entrada del teclado para los ejes X y Z.
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Crear un vector de movimiento relativo a la rotación de la cámara.
        Vector3 move = transform.right * x + transform.forward * z;

        // Mover el CharacterController.
        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    private void HandleMouseLook()
    {
        // Obtener la entrada del ratón.
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Limitar la rotación vertical de la cámara para evitar un giro completo.
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        // Aplicar la rotación a la cámara y al cuerpo del jugador.
        playerCamera.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void HandleFireInput()
    {
        // Comprobar si se presiona el botón de disparo y si el cooldown ha pasado.
        if (Input.GetButton("Fire1") && Time.time >= lastFireTime + fireCooldown)
        {            
            FireProjectile();
            lastFireTime = Time.time;
        }
    }

    private void FireProjectile()
    {

        if (objectPooler == null || firePoint == null)
        {
            Debug.LogError("ObjectPooler o FirePoint no están asignados.");
            return;
        }

        // Obtener un proyectil de la reserva. Usamos la etiqueta para encontrar el tipo correcto.
        GameObject projectile = objectPooler.SpawnFromPool(projectileTag, firePoint.position, firePoint.rotation);
    }
}