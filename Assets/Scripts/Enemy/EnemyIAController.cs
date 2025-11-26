using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyIAController : MonoBehaviour
{
    // Enumerador para definir los estados de la Máquina de Estados Finita (FSM).
    public enum AIState
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Dying,
        Sleeping
    }

    [Header("AI Settings")]
    [SerializeField] AIState currentState = AIState.Idle;
    [SerializeField] float patrolSpeed = 3f;
    //[SerializeField] float chaseSpeed = 5f;
    [SerializeField] float detectionRange = 10f;
    [SerializeField] float attackRange = 10f;
    // [SerializeField] int attackDamage = 10;
    [SerializeField] float attackRate = 1f;
    [SerializeField] Transform[] patrolWaypoints;
    [SerializeField] int currentWaypointIndex;

    // Referencias a otros componentes y al jugador.
    private NavMeshAgent navMeshAgent;
    private Transform player;
    private PlayerHealth playerHealth;
    private IDamageable playerDamageable; // Usamos la interfaz
    private float nextAttackTime;
    private Animator anim;
    public AIState CurrentState
    {
        get { return currentState; }
        set { currentState = value; }
    }
    public bool HasWayPoints
    {
        get { return patrolWaypoints != null && patrolWaypoints.Length > 0; }
    }
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        GameObject gameObjectPlayer = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponentInChildren<Animator>();

        if (gameObjectPlayer != null)
        {
            player = gameObjectPlayer.transform;
            playerHealth = gameObjectPlayer.GetComponent<PlayerHealth>();

        }
    }

    void Update()
    {
        switch (currentState)
        {
            case AIState.Idle:
                // do idle animation
                //check for player
                if (player != null && Vector3.Distance(transform.position, player.position) <= detectionRange)
                {
                    currentState = AIState.Chase;
                }
                break;

            case AIState.Patrol:
                // do patrol
                if (player != null && Vector3.Distance(transform.position, player.position) <= detectionRange)
                {
                    currentState = AIState.Chase;
                }

                if (gameObject.tag == "EnemyChaser")
                {
                    currentState = AIState.Chase;
                    break;
                }
             if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                {
                    if(patrolWaypoints.Length == 0)
                    {
                        Debug.LogWarning("No hay waypoints asignados para la patrulla.");
                        break;
                    }
                    currentWaypointIndex = (currentWaypointIndex + 1) % patrolWaypoints.Length;
                    navMeshAgent.SetDestination(patrolWaypoints[currentWaypointIndex].position);
                }

                navMeshAgent.speed = patrolSpeed;
               
                break;

            case AIState.Chase:
                // do chase

                if (player != null)
                {
                    //AnimationHandler();
                    // Debug.Log("Distance " + Vector3.Distance(transform.position, player.position) + " AttackRange " + attackRange);
                    
                    // Si el jugador está en rango de ataque, pasamos al estado de ataque.
                    if (Vector3.Distance(transform.position, player.position) <= attackRange)
                    {
                        currentState = AIState.Attack;
                    }
                    // if (transform.tag == "EnemyChaser")
                    //     break;
                    // Si el jugador se aleja, volvemos al estado de patrulla.
                    else if (Vector3.Distance(transform.position, player.position) > detectionRange)
                    {
                        if(patrolWaypoints.Length == 0)
                        {
                            currentState = AIState.Idle;
                            break;
                        }
                        currentState = AIState.Patrol;
                    }
                }
                break;

            case AIState.Attack:
                
                // --- Hacer que el enemigo mire hacia el jugador ---
                if (player != null)
                {
                    Vector3 direction = (player.position - transform.position).normalized;
                    direction.y = 0f; // Evita que incline la cabeza hacia arriba/abajo
                    if (direction != Vector3.zero)
                    {
                        Quaternion lookRotation = Quaternion.LookRotation(direction);
                        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
                    }
                }

                // Atacar solo si ha pasado suficiente tiempo desde el último ataque.
                if (playerHealth != null && Time.time >= nextAttackTime)
                {
                    nextAttackTime = Time.time + 1f / attackRate;
                }

                // Si el jugador se aleja, volvemos al estado de persecución.
                if (player != null && Vector3.Distance(transform.position, player.position) > attackRange)
                {
                    currentState = AIState.Chase;
                }
                break;
            case AIState.Sleeping:
                if (navMeshAgent.isActiveAndEnabled) // Asegúrate de que el agente esté activo y habilitado
                {
                    if (navMeshAgent.isOnNavMesh)
                    {
                        navMeshAgent.isStopped = true; 
                    }
                }
                break;
            case AIState.Dying:
                // do chase
                break;

            default:

                break;
        }
        AnimationHandler();

    }

    // Método para la muerte del enemigo.
    public void Die()
    {
        currentState = AIState.Dying;
        Destroy(gameObject, 2f); // Destruye el objeto después de 2 segundos.
    }
    private void AnimationHandler()
    {
        // Velocidad global del agente
        Vector3 velocity = navMeshAgent.velocity;

        // Convertir a espacio local del enemigo
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);

        // Pasar valores al Animator
        anim.SetFloat("SpeedZ", localVelocity.z); // adelante/atrás
        anim.SetFloat("SpeedX", localVelocity.x); // izquierda/derecha

        //Movimiento suavizado
        //anim.SetFloat("SpeedZ", localVelocity.z, 0.1f, Time.deltaTime);
        //anim.SetFloat("SpeedX", localVelocity.x, 0.1f, Time.deltaTime);

    }
    #if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.red;
        // Gizmos.DrawWireSphere(transform.position, retentionRange);
        // línea de vista
        if (player != null)
        {
            Gizmos.DrawLine(transform.position + Vector3.up * 0.9f, player.position + Vector3.up * 0.9f);
        }
    }
#endif
}
