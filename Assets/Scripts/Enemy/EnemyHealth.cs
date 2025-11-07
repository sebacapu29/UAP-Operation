using System.Collections;
using UnityEngine;

public class EnemyHealth : HealthManager, IDamageable
{
    //EnemyIAController enemyIA;
    [SerializeField] GameObject sleepIcon;         // Hijo con SpriteRenderer o Canvas

    [SerializeField] Animator reactionAnimator;       // Opcional: Animator en el icono �Z�
    [SerializeField] float sleepDuration = 20f;
    private SphereCollider campOfVision;
    private EnemyLookAt enemyLookAt;
    private Coroutine sleepRoutine;
    internal bool isEnemySleeped = false;
    private void Awake()
    {
        enemyIA = GetComponent<EnemyIAController>();
        campOfVision = GetComponent<SphereCollider>();
        // sleepIcon.SetActive(false);

        enemyLookAt = GetComponent<EnemyLookAt>();
    }

    /// <summary>
    /// Lanza el estado �dormido� con animaci�n y timer.
    /// </summary>
    public void Sleep()
    {
        isEnemySleeped = true;
        // Si ya hab�a una rutina, la detenemos
        if (sleepRoutine != null)
            StopCoroutine(sleepRoutine);

        sleepRoutine = StartCoroutine(DoSleepRoutine());
    }
    public override void Death()
    {
        base.Death();
    }
    private IEnumerator DoSleepRoutine()
    {
        // 1. Estado Idle y icono activo
        enemyIA.CurrentState = EnemyIAController.AIState.Sleeping;
        sleepIcon.SetActive(true);
        if (enemyLookAt != null)
        {
            enemyLookAt.EnemySleeped = true;
        }
        // 2. Disparamos animaci�n (si existe)
        //if (sleepAnimator != null)
        //    sleepAnimator.SetTrigger("StartSleep");

        //2.1 Desactivamos el campo de visi�n para que no despierte
        campOfVision.enabled = true;

        // 3. Esperamos el timer
        yield return new WaitForSeconds(sleepDuration);

        // 4. Volvemos a Patrol, ocultamos el icono y reactivamos visi�n
        enemyIA.CurrentState = EnemyIAController.AIState.Patrol;
        sleepIcon.SetActive(false);
        campOfVision.enabled = true;
        sleepRoutine = null;
        if (enemyLookAt != null)
        {
            enemyLookAt.EnemySleeped = false;
        }
    }

    /// <summary>
    /// Fuerza despertar inmediato (cancela rutina y ajusta estado).
    /// </summary>
    public void WakeUp()
    {
        isEnemySleeped = false;
        if (sleepRoutine != null)
            StopCoroutine(sleepRoutine);

        enemyIA.CurrentState = EnemyIAController.AIState.Chase; // o el estado que prefieras
        sleepIcon.SetActive(false);
        campOfVision.enabled = true;
        sleepRoutine = null;
    }
}
