using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2MG : MonoBehaviour
{
     [Header("Configuración Cámara Isométrica")]
    public Vector3 cameraOffset = new Vector3(5, 8, -5);
    public float cameraSmoothness = 5f;

    [Header("Referencias")]
    public Transform player; // ← ASIGNAR MANUALMENTE

    void Start()
    {
        // Si no se asignó manualmente, buscar por tag
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        
        // Posicionar cámara inicial
        transform.position = player.position + cameraOffset;
        transform.LookAt(player.position);
    }

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 targetPosition = player.position + cameraOffset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSmoothness * Time.deltaTime);
        transform.LookAt(player.position);
    }
}
