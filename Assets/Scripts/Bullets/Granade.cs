using UnityEngine;
using System.Collections;

public class Grenade : MonoBehaviour
{
    [Header("Gas Settings")]
    [SerializeField] float delay = 4f; // Tiempo antes de liberar el gas
    [SerializeField] float effectRadius = 5f; // Radio del gas
    // [SerializeField] float sleepDuration = 5f; // Tiempo que el enemigo queda dormido

    [Header("Effects")]
    [SerializeField] GameObject gasEffectPrefab;
    [SerializeField] AudioClip gasSound;
    // [SerializeField] float destroyDelay = 2f;

    private bool hasReleasedGas = false;

    void Start()
    {
        Invoke(nameof(ReleaseGas),0f);
        Invoke(nameof(ImpactGasOnEnemies), delay);
        Invoke(nameof(EmptyGas), delay + 10f);
    }
    void ImpactGasOnEnemies()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, effectRadius);
        foreach (Collider nearbyObject in colliders)
        {
            EnemyHealth enemy = nearbyObject.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.Sleep();
            }
        }
    }   
    void EmptyGas()
    {
       Destroy(gameObject);
    }
    void ReleaseGas()
    {
         if (hasReleasedGas) return;
        hasReleasedGas = true;

        AudioManager.Instance.Play("Explosion");
    }

    // IEnumerator SleepEnemy(EnemyAI enemy)
    // {
    //     enemy.Sleep(); // Método que desactiva NavMeshAgent y lógica
    //     yield return new WaitForSeconds(sleepDuration);
    //     enemy.WakeUp(); // Método que reactiva todo
    // }
}