using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance { get; private set; }

    [Header("Configuración del Pool")]
    [Tooltip("Prefab del enemigo que se va a clonar")]
    [SerializeField] GameObject enemyPrefab;

    [Tooltip("Número inicial de enemigos en el pool")]
    [SerializeField] int initialPoolSize = 10;

    [Tooltip("Número de enemigos a agregar cuando el pool se queda sin objetos")]
    [SerializeField] int poolIncrement = 5;

    private Queue<GameObject> availableEnemies = new Queue<GameObject>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializePool();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InitializePool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            AddEnemyToPool();
        }
    }

    void AddEnemyToPool()
    {
        // GameObject enemy = Instantiate(enemyPrefab);
        // enemy.SetActive(false);
        // availableEnemies.Enqueue(enemy);
    }

    public GameObject GetEnemy(Vector3 position)
    {
        if (availableEnemies.Count == 0)
        {
            Debug.LogWarning("El pool de enemigos se ha quedado sin objetos. Se añadirán más.");
            for (int i = 0; i < poolIncrement; i++)
            {
                AddEnemyToPool();
            }
        }

        GameObject enemy = availableEnemies.Dequeue();
        enemy.transform.position = position;
        enemy.SetActive(true);
        return enemy;
    }

    public void ReturnEnemy(GameObject enemyToReturn)
    {
        // Evita devolver objetos inactivos.
        if (!enemyToReturn.activeSelf)
        {
            enemyToReturn.SetActive(true);
        }

        enemyToReturn.SetActive(false);
        availableEnemies.Enqueue(enemyToReturn);
    }
}
