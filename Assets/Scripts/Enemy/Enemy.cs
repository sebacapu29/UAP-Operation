using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public static event Action<Enemy> OnEnemyDefeated;
    private NavMeshAgent agent;


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent no encontrado en el enemigo.");
        }
    }
    public void Defeat()
    {
        if (agent != null)
        {
            agent.isStopped = true;
            agent.ResetPath();
        }

        OnEnemyDefeated?.Invoke(this);
        gameObject.SetActive(false);
    }
}
