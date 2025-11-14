using UnityEngine;

public class Grenade : MonoBehaviour
{
    [Header("Explosion Settings")]
    [SerializeField] float delay = 3f; // Tiempo antes de explotar
    [SerializeField] float explosionRadius = 5f; // Radio de daño
    [SerializeField] float explosionForce = 700f; // Fuerza física
    [SerializeField] int damage = 50; // Daño a enemigos

    [Header("Effects")]
    [SerializeField] GameObject explosionEffectPrefab; // Partículas
    [SerializeField] AudioClip explosionSound; // Sonido
    [SerializeField] float destroyDelay = 2f; // Tiempo para destruir efectos

    private bool hasExploded = false;

    void Start()
    {
        Invoke(nameof(Explode), delay);
    }

    void Explode()
    {
        if (hasExploded) return;
        hasExploded = true;

        // Instanciar efecto visual
        if (explosionEffectPrefab != null)
        {
            GameObject effect = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, destroyDelay);
        }

        // Reproducir sonido
        if (explosionSound != null)
        {
            AudioManager.Instance.Play("Explosion");
        }

        // Aplicar fuerza y daño en área
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            // if (rb != null)
            // {
            //     rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            // }

            // Si es enemigo, aplicar daño
            EnemyHealth enemy = nearbyObject.GetComponent<EnemyHealth>();
            // Debug.Log("Enemy found: " + enemy); 
            if (enemy != null)
            {
                enemy.Sleep();
            }
        }

        // Destruir la granada
        // Destroy(gameObject);
    }
}
