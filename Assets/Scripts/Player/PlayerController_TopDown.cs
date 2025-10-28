using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController_TopDown : MonoBehaviour
{

    [Header("Movement Settings")]
    [Tooltip("Velocidad de movimiento del jugador.")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform target;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject flashLight;
    [SerializeField] float rotationSpeed = 5f;

    private Rigidbody rb;
    private Camera mainCamera;
    //private float lastFireTime;
    //MiraController miraController;
    //private ObjectPooler objectPooler;
    private Animator anim;
    private float speed=0f;
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
            flashLight.SetActive(!flashLight.activeInHierarchy);
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
        anim.SetFloat("SpeedZ", verticalInput);
        anim.SetFloat("SpeedX", horizontalInput);



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

    //private void HandleFireInput()
    //{
    //    // Comprobar si se presiona el botón de disparo y si el cooldown ha pasado.
    //    if (Input.GetButton("Fire1") && Time.time >= lastFireTime + fireCooldown)
    //    {
    //        FireProjectile();
    //        lastFireTime = Time.time;
    //    }
    //}

    //private void FireProjectile()
    //{

    //    if (objectPooler == null || firePoint == null)
    //    {
    //        Debug.LogError("ObjectPooler o FirePoint no están asignados.");
    //        return;
    //    }

    //    // Obtener un proyectil de la reserva. Usamos la etiqueta para encontrar el tipo correcto.
    //    GameObject projectile = objectPooler.SpawnFromPool(projectileTag, firePoint.position, firePoint.rotation);


    //}
}
