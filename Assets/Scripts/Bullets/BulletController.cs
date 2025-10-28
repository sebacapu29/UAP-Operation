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
            // Aplicar la velocidad al Rigidbody.
            bulletRB.velocity = transform.forward * projectileSpeed;
        }  
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

    // Usamos OnTriggerEnter para detectar colisiones cuando el objeto
    //void OnCollisionEnter(Collision other)
    //{
    //    // Intentar obtener el componente IDamageable del objeto con el que chocamos.
    //    HealthManager healthManager = other.gameObject.GetComponent<HealthManager>();
    //    //IDamageable damageableObject = enemyHealth.GetComponent<IDamageable>();

    //    if (healthManager != null)
    //    {
    //        healthManager.TakeDamage(damageAmount);
    //    }

    //    // Devolver el proyectil a la reserva de objetos para su reutilización.
    //    // Asumimos que ObjectPooler es un Singleton con acceso global.
    //    ObjectPooler.Instance.ReturnToPool(this.gameObject);
    //}
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
    private void OnTriggerEnter(Collider other)
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyChaser"))
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

            if(this.tag == "Sedative")
            {
                enemyHealth?.Sleep();
            }
            //if (enemyHealth != null)
            //{
            //    enemyHealth.TakeDamage(damageAmount);
            //}
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
