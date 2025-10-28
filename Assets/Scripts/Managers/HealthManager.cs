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

    virtual public void Death()
    {
        Destroy(gameObject);
    }
}
