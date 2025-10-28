using UnityEngine;

public class PathGrid : MonoBehaviour
{
    public Vector2 gridWorldSize;   // Tamaño del grid en el mundo (X, Y)
    public float nodeRadius;        // Radio de cada nodo
    public LayerMask unwalkableMask; // Capa de obstáculos
    Node1[,] grid;                   // Matriz de nodos

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node1[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position 
                                  - Vector3.right * gridWorldSize.x / 2 
                                  - Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft 
                                     + Vector3.right * (x * nodeDiameter + nodeRadius) 
                                     + Vector3.forward * (y * nodeDiameter + nodeRadius);

                // Detecta si hay un obstáculo en ese punto
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));

                grid[x, y] = new Node1(walkable, worldPoint, x, y);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        if (grid != null)
        {
            foreach (Node1 n in grid)
            {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;

                // Dibujar cubo con un pequeño "padding" para que no se vean pegados
                float separation = 0.1f; // ajusta este valor a gusto
                Vector3 cubeSize = Vector3.one * (nodeDiameter - separation);

                Gizmos.DrawCube(n.worldPosition, cubeSize);
            }
        }
    }
}