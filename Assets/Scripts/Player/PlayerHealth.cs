using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : HealthManager
{
    public int Health { get { return health; } }

    private bool isDead = false;
    private bool sceneLoaded = false;
    private Animator playerAnimator;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Detectar si el jugador lo atraviesa una bala
        if(other.CompareTag("Bullet"))
        {
            //Intentar hacer daño
            IDamageable damageable = other.GetComponent<IDamageable>();
            if(damageable != null)
            {
                damageable.TakeDamage(10); //Daño fijo por ahora
            }
            if(health <= 0)
            {
                Death();
            }
        }
    }

    void Update()
    {
        if(health <=0) UIOnGUINew.Instance.UpdateCollectedItems("Health", 0);
        else UIOnGUINew.Instance.UpdateCollectedItems("Health", Health);
        if (!isDead || sceneLoaded) return;
        if (playerAnimator == null) return;
        // Verificar si la animación de "Death" terminó (normalizedTime >= 1)
        AnimatorStateInfo state = playerAnimator.GetCurrentAnimatorStateInfo(0);
        if (state.IsName("PlayerDeath") && state.normalizedTime >= 1f)
        {
            sceneLoaded = true;
            SceneManager.LoadScene("EndingGameOver");
        }
    }

    override public void Death()
    {
        if (isDead) return;
        isDead = true;

        Debug.LogWarning("The Player is Death");
        GameManager.Instance.isGameOver = true;
        if (playerAnimator == null) playerAnimator = GetComponent<Animator>();
        playerAnimator.SetTrigger("Death");
        //Player respawn
    }
}