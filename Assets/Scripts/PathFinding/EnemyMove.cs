using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [Tooltip("La velocidad constante a la que se moverá el enemigo.")]
    public float moveSpeed = 3f;

    [Tooltip("La dirección inicial del movimiento (ej. Vector2.right para la derecha).")]
    public Vector2 moveDirection = Vector2.right;

    [Header("Debugging y Activación")]
    [Tooltip("Tecla para activar el movimiento del enemigo.")]
    public KeyCode activationKey = KeyCode.K;

    private Rigidbody rb;
    private bool isActivated = false;

    void Start()
    {
       
        rb = GetComponent<Rigidbody>();
        
        if (rb == null)
        {
            Debug.LogError("El enemigo necesita un componente Rigidbody2D para funcionar.");
        }

        rb.freezeRotation = true;
    }

    void Update()
    {

        if (Input.GetKeyDown(activationKey))
        {
            isActivated = !isActivated;
            Debug.Log("Movimiento del enemigo: " + (isActivated ? "ACTIVADO" : "DESACTIVADO"));
        }
    }

    void FixedUpdate()
    {
        if (isActivated)
        {

            rb.velocity = moveDirection.normalized * moveSpeed;
        }
        else
        {

            rb.velocity = Vector2.zero;
        }
    }
}

