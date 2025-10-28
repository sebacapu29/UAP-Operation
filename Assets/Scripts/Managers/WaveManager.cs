using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        [Tooltip("Numero de enemigos en esta oleada ")]
        public int numberOfEnemies = 3;

        [Tooltip("Tiempo de espera antes de que aparezca la siguiente oleada")]        
        public float timeBetweenWaves = 5f;
    }

    [Header("Configuracion de las Oleadas")]
    [Tooltip("Lista de oleadas a generar")]
    public List<Wave> spawnerWaves = new List<Wave>();

    [Tooltip("Punto de spawn de los enemigos")]
    public Transform spawnPoint;
    
    private int currentWaveIndex = 0;
    private int enemiesDefeatedInCurrentWave = 0;
    private float timeToNextWave = 0f;
   private float _reductionInterval = 2f; // cada cuántos segundos

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Enemy.OnEnemyDefeated += HandleEnemyDefeated;
    }
    void OnDestroy()
    {
        Enemy.OnEnemyDefeated -= HandleEnemyDefeated;
    }
    //Crea la funcion IEnumarator de StartWaves con indexWave como argumento
    private System.Collections.IEnumerator StartWaves(int indexWave)
    {
        if (indexWave >= spawnerWaves.Count)
        {
            Debug.Log("Todas las oleadas han sido completadas.");
            yield break; // Sale de la corrutina si no hay más oleadas
        }
        Debug.Log($"Iniciando oleada {indexWave + 1} con {spawnerWaves[indexWave].numberOfEnemies} enemigos.");

        Wave currentWave = spawnerWaves[indexWave];
        enemiesDefeatedInCurrentWave = 0;
        for (int i = 0; i < currentWave.numberOfEnemies; i++)
        {
            // Llama al EnemySpawner para que genere un enemigo
            EnemyPool.Instance.GetEnemy(spawnPoint.position);
            yield return new WaitForSeconds(1f); // Espera 1 segundo entre cada spawn
        }
    }
    void HandleEnemyDefeated(Enemy defeatedEnemy)
    {
        enemiesDefeatedInCurrentWave++;
        Debug.Log("Enemigos derrotados en la oleada actual: " + enemiesDefeatedInCurrentWave);

        if (enemiesDefeatedInCurrentWave >= spawnerWaves[currentWaveIndex].numberOfEnemies)
        {
            Debug.Log($"Oleada {currentWaveIndex + 1} completada.");
            currentWaveIndex++;
            StartCoroutine(WaitAndStartNextWave(spawnerWaves[currentWaveIndex - 1].timeBetweenWaves));
        }
    }
    
    private IEnumerator WaitAndStartNextWave(float waitTime)
    {
        Debug.Log($"Esperando {waitTime} segundos antes de iniciar la siguiente oleada.");
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(StartWaves(currentWaveIndex));
    }
    // Update is called once per frame
    void Update()
    {
        timeToNextWave += Time.deltaTime;
        var playerIsDiscovered = LevelManager.Instance.activateWaveEnemy && currentWaveIndex == 0;
        if (timeToNextWave >= _reductionInterval && playerIsDiscovered)
        {
            timeToNextWave = 0f;
            StartCoroutine(StartWaves(currentWaveIndex));
        }
            
        
    }
}
