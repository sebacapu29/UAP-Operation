using UnityEngine;

// Script auxiliar para testear la obtención de Nodos en la cuadrícula.
public class TestingGrid : MonoBehaviour
{
    private MapGrid gridManager;

    void Start()
    {
        // Obtener la única instancia del Grid Manager en la escena.
        gridManager = FindFirstObjectByType<MapGrid>();

        if (gridManager == null)
        {
            Debug.LogError("Grid Manager no encontrado en la escena.");
            return;
        }
    }

    void Update()
    {
        // Presionar 'T' para obtener y mostrar la información del nodo actual.
        if (Input.GetKeyDown(KeyCode.T))
        {
            // Convertir la posición mundial del objeto al nodo de la cuadrícula.
            Node currentNode = gridManager.NodeFromWorldPoint(transform.position);

            if (currentNode != null)
            {
                Debug.Log($"Nodo actual: ({currentNode.gridX}, {currentNode.gridY}) - Transitable: {currentNode.isWalkable}");
            }
        }
    }
}
