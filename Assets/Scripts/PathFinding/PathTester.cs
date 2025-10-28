using UnityEngine;

// Este script solicita una ruta al algoritmo Pathfinder.
public class PathTester : MonoBehaviour
{
    [Header("Objetos de Prueba")]
    [Tooltip("El objeto que inicia la ruta (ej. el Enemigo).")]
    public Transform pathStart;
    [Tooltip("El objeto objetivo de la ruta (ej. el Jugador).")]
    public Transform pathTarget;

    private MapGrid gridManager;

    void Start()
    {
        gridManager = GetComponent<MapGrid>();
    }

    void Update()
    {
        // Presionar 'P' para calcular y dibujar la ruta entre los dos objetos.
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (pathStart != null && pathTarget != null)
            {
                // Llamamos directamente al método estático FindPath del algoritmo.
                // En un sistema real, usaríamos un PathRequestManager para la cola.
                Pathfinder.FindPath(pathStart.position, pathTarget.position, gridManager);
//                Debug.Log("Ruta calculada y dibujada (Gizmos).");
            }
            else
            {
        //        Debug.LogWarning("¡Asigna los objetos Start y Target en el Inspector!");
            }
        }
    }
}


