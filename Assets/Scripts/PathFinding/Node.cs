using UnityEngine;

public class Node 
{
    
    // Las coordenadas x, y en la matriz (no la posición global).
    public int gridX; 
    public int gridY;

    // Posición real del nodo en el mundo 3D de Unity.
    public Vector3 worldPosition;

    // Determina si se puede caminar por este nodo.
    public bool isWalkable;

    // Costo desde el nodo inicial.
    public int gCost;
    // Costo estimado hasta el nodo final (Heurística).
    public int hCost;
    // Costo total (fCost = gCost + hCost).
    public int fCost { get { return gCost + hCost; } }

    // Referencia al nodo anterior en el camino.
    public Node parent;

    public Node(bool walkable, Vector3 worldPos, int x, int y)
    {
        isWalkable = walkable;
        worldPosition = worldPos;
        gridX = x;
        gridY = y;
    }
    
    // ----------------------------------------------------------------------
    // MÉTODOS REQUERIDOS POR HASHSET
    // ----------------------------------------------------------------------

    // Necesario para que el HashSet pueda identificar y comparar nodos rápidamente.
    // Dos nodos son "iguales" si tienen las mismas coordenadas en la cuadrícula.
    public override bool Equals(object obj)
    {
        // 1. Verificación básica.
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Node other = (Node)obj;
        // 2. Compara solo las coordenadas de la cuadrícula, no la posición mundial ni el costo.
        return gridX == other.gridX && gridY == other.gridY;
    }

    // Genera un código hash único basado en las coordenadas X e Y.
    // Esto permite al HashSet ir directamente a la ubicación de almacenamiento del nodo (O(1)).
    public override int GetHashCode()
    {
        // Se usa una fórmula simple y rápida para generar el hash.
        return (gridX * 487) ^ gridY; 
    }
}

