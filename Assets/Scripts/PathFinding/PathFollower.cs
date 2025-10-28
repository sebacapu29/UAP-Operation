using UnityEngine;
using System.Collections;
using System.Collections.Generic; 


public class PathFollower : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [Tooltip("Velocidad a la que el objeto sigue la ruta.")]
    public float speed = 5f;
    [Tooltip("La distancia mínima para considerar que el objeto ha llegado al nodo.")]
    public float nextNodeDistanceThreshold = 0.1f;

   
    private Vector3[] pathWaypoints;
    private int targetIndex;
    private bool isMoving = false;

    
    public void StartFollowingPath(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful && newPath.Length > 0)
        {
            pathWaypoints = newPath;
            targetIndex = 0;
            isMoving = true;
            

            StopAllCoroutines(); 
            StartCoroutine(FollowPathCoroutine());
            Debug.Log("Iniciando seguimiento de ruta. Nodos: " + newPath.Length);
        }
        else
        {
             Debug.LogWarning("La ruta es inválida o no se encontró camino. No se inicia el seguimiento.");
             isMoving = false;
        }
    }

    
    IEnumerator FollowPathCoroutine()
    {
 
        Vector3 currentWaypoint = pathWaypoints[0];

        while (isMoving)
        {
      
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);

         
            if (Vector3.Distance(transform.position, currentWaypoint) < nextNodeDistanceThreshold)
            {
                targetIndex++;
                
                
                if (targetIndex >= pathWaypoints.Length)
                {
                    Debug.Log("Ruta completada.");
                    isMoving = false;
                    yield break;
                }

               
                currentWaypoint = pathWaypoints[targetIndex];
            }

            
            yield return null; 
        }
    }
}
