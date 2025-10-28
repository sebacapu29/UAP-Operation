using System.Collections.Generic;
using UnityEngine;
using System; 


// Clase estática que implementa el algoritmo de búsqueda de ruta A*.
public static class Pathfinder
{
     // Método principal para encontrar la ruta entre dos puntos.
    // **IMPORTANTE:** Recibe la instancia de Grid que se va a utilizar.
    public static void FindPath(Vector3 startPos, Vector3 targetPos, MapGrid mapGrid)
    {
        Node startNode = mapGrid.NodeFromWorldPoint(startPos);
        Node targetNode = mapGrid.NodeFromWorldPoint(targetPos);

        if (startNode == null || targetNode == null || !targetNode.isWalkable)
        {
            Debug.LogError("Pathfinding falló: Nodos de inicio o destino no válidos.");
            return;
        }

        // --- Inicialización de colecciones ---
        List<Node> openSet = new List<Node>(); 
        HashSet<Node> closedSet = new HashSet<Node>(); 
        
        openSet.Add(startNode);

        // --- Bucle principal del algoritmo A* ---
        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];

            // Encuentramos el nodo con el menor fCost (y menor hCost para desempate).
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || (openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost))
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                // Reconstruir la ruta y terminar.
                mapGrid.path = ReconstructPath(startNode, targetNode);
                return;
            }

            // Exploramos los vecinos
            foreach (Node neighbour in GetNeighbours(currentNode, mapGrid))
            {
                if (!neighbour.isWalkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);

                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode; // Establecemos el nodo actual como el padre.

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Reconstruye la ruta final, retrocediendo desde el nodo final hasta el inicio.
    /// </summary>
    private static List<Node> ReconstructPath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        // Invertimos la lista para que el camino vaya del inicio al final.
        path.Reverse();
        return path;
    }
    
    /// <summary>
    /// Obtiene todos los vecinos del nodo actual (8 direcciones).
    /// </summary>
    private static List<Node> GetNeighbours(Node node, MapGrid mapGrid)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue; // No incluir el nodo central.

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                // Asegura que el vecino esté dentro de los límites de la cuadrícula.
                if (checkX >= 0 && checkX < mapGrid.gridSizeX && checkY >= 0 && checkY < mapGrid.gridSizeY)
                {
                    // Usamos 'grid' para acceder a la matriz grid.
                    neighbours.Add(mapGrid.grid[checkX, checkY]);
                }
            }
        }
        return neighbours;
    }

    /// <summary>
    /// Calcula la distancia (costo H) entre dos nodos usando el método Manhattan/Diagonal.
    /// </summary>
    private static int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            // 14 es el costo diagonal, 10 es el costo horizontal/vertical.
            return 14 * dstY + 10 * (dstX - dstY); 
        
        return 14 * dstX + 10 * (dstY - dstX);
    }
}


