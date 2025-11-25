using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletController : MonoBehaviour
{
    // Propiedades del proyectil, visibles en el Inspector.
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] int damageAmount = 25;
    [SerializeField] float timeAlive = 2f;
    private Vector3 direction;

    Rigidbody bulletRB;

    void Awake()
    {
        // Obtener el Rigidbody.
        bulletRB = GetComponent<Rigidbody>();
    }

    private void OnEnable() 
    {
        
        if (bulletRB != null)
        {
            var enemyPosition = GetEnemyPosition();
            var shootDirection = (enemyPosition - transform.position).normalized;
            // Aplicar la velocidad al Rigidbody.
            bulletRB.velocity = shootDirection * projectileSpeed;
        }  
    }
    Vector3 GetEnemyPosition()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 6f);
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
        return transform.forward;
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
