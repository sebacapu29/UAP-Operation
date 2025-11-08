using System.Collections;
using UnityEngine;

public class HealthManager : MonoBehaviour, IDamageable
{
    [SerializeField] protected int health = 100;
    protected EnemyIAController  enemyIA;
    private void Awake()
    {
        enemyIA = GetComponent<EnemyIAController>();
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        //Animacion de daño
        Debug.Log("Salud :" + gameObject.name + " " + health);

        if (health <= 0)
        {
            Death();
        }
        //else
        //{
        //    enemyIA = GetComponent<EnemyIAController>();
        //    enemyIA.CurrentState = EnemyIAController.AIState.Patrol;
        //}
    }
     public void Heal(int amount)
{
    // Aumenta la salud por la cantidad especificada
    health += amount;
    
    // Opcional: Puedes añadir un límite máximo de salud si lo deseas
    // Por ejemplo: if (health > maxHealth) { health = maxHealth; }
    
    Debug.Log("Salud curada. Nueva salud de " + gameObject.name + ": " + health);
    // Opcional: Añadir una animación o efecto de curación aquí
}
    virtual public void Death()
    {
        Destroy(gameObject);
    }
}
