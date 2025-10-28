using UnityEngine;

public class PlayerHealth : HealthManager
{

    public int Health { get { return health; } }
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
        }
    }
    override public void Death()
    {
        Debug.LogWarning("The Player is Death");
        GameManager.Instance.isGameOver = true;
        //Player respawn
    }
}
