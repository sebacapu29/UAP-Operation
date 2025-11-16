using UnityEngine;
using System.Collections;

public class Grenade : MonoBehaviour
{
    [Header("Gas Settings")]
    [SerializeField] float delay = 3f; // Tiempo antes de liberar el gas
    [SerializeField] float effectRadius = 5f; // Radio del gas
    [SerializeField] float sleepDuration = 5f; // Tiempo que el enemigo queda dormido

    [Header("Effects")]
    [SerializeField] GameObject gasEffectPrefab;
    [SerializeField] AudioClip gasSound;
    [SerializeField] float destroyDelay = 2f;

    private bool hasReleasedGas = false;

    void Start()
    {
        Invoke(nameof(ReleaseGas), delay);
    }

    void ReleaseGas()
    {
        if (hasReleasedGas) return;
        hasReleasedGas = true;

        // Instanciar efecto visual
        if (gasEffectPrefab != null)
        {
            GameObject effect = Instantiate(gasEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, destroyDelay);
        }

        // Sonido
        // if (gasSound != null)
        // {
        //     AudioSource.PlayClipAtPoint(gasSound, transform.position);
        // }
        AudioManager.Instance.Play("Explosion");
        // Detectar enemigos en el radio
        Collider[] colliders = Physics.OverlapSphere(transform.position, effectRadius);
        foreach (Collider nearbyObject in colliders)
        {
            EnemyHealth enemy = nearbyObject.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.Sleep();
            }
        }

        Destroy(gameObject);
    }

    // IEnumerator SleepEnemy(EnemyAI enemy)
    // {
    //     enemy.Sleep(); // Método que desactiva NavMeshAgent y lógica
    //     yield return new WaitForSeconds(sleepDuration);
    //     enemy.WakeUp(); // Método que reactiva todo
    // }
}