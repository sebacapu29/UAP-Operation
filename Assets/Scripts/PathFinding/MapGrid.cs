using UnityEngine;
using System.Collections.Generic;

// Este script crea y gestiona la representación del mapa
// como una cuadrícula de nodos.
public class MapGrid : MonoBehaviour
{
    [Header("Configuración del Mapa")]
    // Capa que contiene los obstáculos (ej. "Default" o "Walls"). Se asigna en el Inspector.
    public LayerMask unwalkableMask; 
    // Tamaño total del área de búsqueda (eje X, Z).
    public Vector2 gridWorldSize; 
    // Radio de cada nodo.
    public float nodeRadius; 
    
    // Array bidimensional para almacenar todos los nodos.
    private Node[,] _grid; 
    private float nodeDiameter;
    private int _gridSizeX, _gridSizeY;
    
    public int gridSizeX { get { return _gridSizeX; }}

    public int gridSizeY { get { return _gridSizeY; }}

    public Node[,] grid { get { return _grid; }}


    // Lista para almacenar la ruta final encontrada (para visualización).
    [HideInInspector] public List<Node> path;

    void OnValidate()
    {
        // Forzamos la reconstrucción de la cuadrícula en el editor.
        SetupAndCreateGrid();
    }


    void Awake()
    {
        SetupAndCreateGrid();
    }

    // Crea la cuadrícula y define qué nodos son transitables.
    void CreateGrid()
    {
        _grid = new Node[_gridSizeX, _gridSizeY];
        // Determina el punto de inicio (esquina inferior izquierda) del mapa.
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < _gridSizeX; x++)
        {
            for (int y = 0; y < _gridSizeY; y++)
            {
                // Calcula la posición mundial de cada nodo.
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                
                // Usamos Physics.CheckSphere para detectar colisiones con obstáculos.
                // Si la esfera choca con algo en la capa 'unwalkableMask', el nodo no es transitable.
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                
                _grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    // Método de utilidad para obtener el nodo correspondiente a una posición mundial.
    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        // Convierte la posición mundial a un porcentaje (0 a 1) dentro de la cuadrícula.
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
        
        // Asegura que los valores estén limitados entre 0 y 1.
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        // Convierte el porcentaje a índices de matriz.
        int x = Mathf.RoundToInt((_gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((_gridSizeY - 1) * percentY);
        return _grid[x, y];
    }
    
    // Dibuja la representación visual del grafo en el editor de Unity (Solo visible en la Scene view).
    void OnDrawGizmos()
    {
        // Dibuja los límites del mapa
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

         if (_grid == null)
        {
            SetupAndCreateGrid();
        }

        if (_grid != null)
        {
            foreach (Node n in _grid)
            {
                // Establece el color de visualización (Rojo si es obstáculo, Blanco si es transitable).
                Gizmos.color = (n.isWalkable) ? Color.white : Color.red;

                // Si el nodo está en la ruta final (calculada por Pathfinder), lo pintamos de verde.
                if (path != null && path.Contains(n))
                {
                    Gizmos.color = Color.green;
                }

                // Dibuja el cubo del nodo en su posición mundial. El -0.1f es para que no se peguen.
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .05f));
            }
        }
    }

    void SetupAndCreateGrid()
    {
        if (nodeRadius <= 0 || gridWorldSize.x <= 0 || gridWorldSize.y <= 0) return;
        
        nodeDiameter = nodeRadius * 2;
        
        // CÁLCULO SEGURO DE DIMENSIONES (Usando CeilToInt para cubrir todo el mapa)
        _gridSizeX = Mathf.CeilToInt(gridWorldSize.x / nodeDiameter);
        _gridSizeY = Mathf.CeilToInt(gridWorldSize.y / nodeDiameter);

        if (_gridSizeX <= 0 || _gridSizeY <= 0) return;
        
        CreateGrid();
    }
}




