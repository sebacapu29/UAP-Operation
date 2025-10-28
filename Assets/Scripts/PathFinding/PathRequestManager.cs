using UnityEngine;
using System.Collections.Generic;


public class PathRequestManager : MonoBehaviour
{
    private Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    private PathRequest currentPathRequest;

    public static PathRequestManager Instance;

    private bool isProcessingPath;

    void Awake()
    {
        if (Instance != null)
        {    
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Estructura para almacenar la información de una petición de ruta.
    struct PathRequest
    {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public System.Action<List<Node>, bool> callback;

        public PathRequest(Vector3 _start, Vector3 _end, System.Action<List<Node>, bool> _callback)
        {
            pathStart = _start;
            pathEnd = _end;
            callback = _callback;
        }
    }

    // Método que los enemigos llaman para pedir una ruta.
    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, System.Action<List<Node>, bool> callback)
    {
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
        Instance.pathRequestQueue.Enqueue(newRequest);
        Instance.TryProcessNext();
    }

    // Intenta procesar la siguiente solicitud en la cola.
    void TryProcessNext()
    {
        if (!isProcessingPath && pathRequestQueue.Count > 0)
        {
            currentPathRequest = pathRequestQueue.Dequeue();
            isProcessingPath = true;
            // Llamamos al algoritmo A* en un hilo de trabajo o Coroutine (aquí lo haremos directo para simplificar).
            Pathfinder.FindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd, FindFirstObjectByType<MapGrid>()); 
        }
    }

    // Llamado por el Pathfinder una vez que encuentra la ruta.
    public void FinishedProcessingPath(List<Node> path, bool success)
    {
        currentPathRequest.callback(path, success);
        isProcessingPath = false;
        TryProcessNext();
    }
}


