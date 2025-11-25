using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float _bulletSpeed = 25;
    [SerializeField] int _bulletDamage = 15;

    Rigidbody rg;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rg = GetComponent<Rigidbody>();
        rg.velocity = this.transform.forward * _bulletSpeed;

        Destroy(gameObject,2f);
        
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.transform.CompareTag("Player"))
        {
            
            PlayerHealth playerHealth;
            playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            
            if (playerHealth != null)
            {
                AudioManager.Instance.Play("PlayerHurt");
                playerHealth.TakeDamage(_bulletDamage);
            }
        }

        Destroy(gameObject);    
    }
}
