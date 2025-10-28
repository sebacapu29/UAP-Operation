using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


// [System.Serializable] hace que la clase sea visible en el Inspector.
[System.Serializable]
public class Pool
{
    public string tag;
    public GameObject prefab;
    public int size;
}

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    public List<Pool> pools;

    private Dictionary<string, Queue<GameObject>> poolDictionary;
    [SerializeField] Transform firePoint;
    [SerializeField] Transform target;
    void Awake()
    {
        // Implementación del patrón Singleton.
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        // No destruir al cargar una nueva escena.
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

       
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.transform.SetParent(transform, true);
                
                obj.SetActive(false); // Desactivar el objeto al crearlo.
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    // Método para obtener un objeto de la reserva.
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool con tag " + tag + " no existe.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        // Activarlo y reubicarlo.
        
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.SetActive(true);
        
        return objectToSpawn;
    }

    // Método para devolver un objeto a la reserva.
    public void ReturnToPool(GameObject objectToReturn)
    {
        
        objectToReturn.SetActive(false);
        // Devolverlo a la cola para su reutilización.
        firePoint.LookAt(target.position);
        poolDictionary[objectToReturn.tag].Enqueue(objectToReturn);
    }
}
