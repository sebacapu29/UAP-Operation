using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletController : MonoBehaviour
{
    // Propiedades del proyectil, visibles en el Inspector.
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] int damageAmount = 25;
    [SerializeField] float timeAlive = 2f;
    [SerializeField] Transform bulletSpawner; // <- spawner point (puedes asignarlo en el Inspector)
    private Vector3 direction;

    Rigidbody bulletRB;

    void Awake()
    {
        // Obtener el Rigidbody.
        bulletRB = GetComponent<Rigidbody>();

        // Intentar asignar el spawner automáticamente si no está enlazado desde el Inspector.
        if (bulletSpawner == null)
        {
            // Primero buscar un hijo llamado "BulletSpawner"
            Transform t = transform.Find("BulletSpawner");
            if (t != null)
                bulletSpawner = t;
            else
            {
                // Si no existe ese hijo, usar el primer hijo distinto del propio transform
                foreach (Transform child in GetComponentsInChildren<Transform>())
                {
                    if (child != transform)
                    {
                        bulletSpawner = child;
                        break;
                    }
                }
            }
        }
    }

    private void OnEnable() 
    {
        if (bulletRB == null) bulletRB = GetComponent<Rigidbody>();

        Vector3 spawnPos = (bulletSpawner != null) ? bulletSpawner.position : transform.position;
        Vector3 spawnForward = (bulletSpawner != null) ? bulletSpawner.forward : transform.forward;

        // Intentar obtener posición de enemigo; si no hay enemigo, disparar en la dirección del spawner.
        Vector3 enemyPosition = GetEnemyPosition();
        Vector3 shootDirection;
        if (enemyPosition != Vector3.zero)
        {
            shootDirection = (enemyPosition - spawnPos).normalized;
        }
        else
        {
            shootDirection = spawnForward.normalized;
        }

        // Aplicar la velocidad al Rigidbody.
        bulletRB.velocity = shootDirection * projectileSpeed;
        bulletRB.rotation = Quaternion.LookRotation(shootDirection);
    }

    Vector3 GetEnemyPosition()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 7f);
        foreach (Collider nearbyObject in colliders)
        {
            EnemyHealth enemy = nearbyObject.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                var enemyState = enemy.GetComponent<EnemyIAController>();
                if (enemyState != null && enemyState.CurrentState != EnemyIAController.AIState.Sleeping)
                    return enemy.gameObject.transform.position;
            }
        }
        return Vector3.zero;
    }
    private void Start()
    {
        Destroy(gameObject, timeAlive);
    }
    void Update()
    {
        //timer += Time.deltaTime;
       
        //if (timer >= timeAlive)
        //    ObjectPooler.Instance.ReturnToPool(this.gameObject);
    }

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection;

        bulletRB = GetComponent<Rigidbody>();

        if (bulletRB != null)
        {
            // Aplicar la velocidad al la bala.
            bulletRB.velocity = direction * projectileSpeed;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyChaser"))
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

            if(this.tag == "Sedative")
            {
                enemyHealth?.Sleep();
                EnemyIAController enemyIA = collision.gameObject.GetComponent<EnemyIAController>();
                enemyIA.CurrentState = EnemyIAController.AIState.Sleeping;
            }
        }
        if (!collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }         
        }
        Destroy(gameObject);
    }
}
