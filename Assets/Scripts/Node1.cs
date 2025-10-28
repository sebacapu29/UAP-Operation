using UnityEngine;
using System.Collections.Generic;

public class Node1
{
    public bool walkable;              // Si el nodo es transitable
    public Vector3 worldPosition;      // Posición en el mundo
    public int gridX;                  // Índice en la grilla (X)
    public int gridY;                  // Índice en la grilla (Y)

    // Para pathfinding (opcional, útil si luego usás A*)
    public int gCost;                  // Costo desde el inicio
    public int hCost;                  // Heurística hacia el objetivo
    public Node1 parent;                // Para reconstruir el camino

    // Constructor
    public Node1(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }

    // fCost = gCost + hCost
    public int fCost
    {
        get { return gCost + hCost; }
    }
}