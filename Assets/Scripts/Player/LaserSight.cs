using UnityEngine;
using static UnityEngine.UI.Image;

[RequireComponent(typeof(LineRenderer))]

public class LaserSight : MonoBehaviour
{
    [Header("Raycast Settings")]
    [Tooltip("Distancia máxima del rayo.")]
    [SerializeField] float maxDistance = 50f;

    [Tooltip("Layers con los que colisiona el rayo.")]
    [SerializeField] LayerMask hitLayers;

    [Header("References")]
    [Tooltip("Cámara que lanza el rayo (opcional).")]
    [SerializeField] Camera mainCamera;

    LineRenderer line;
    [SerializeField] Transform originLaser;
    void Awake()
    {
        // Cacheamos componentes
        line = GetComponent<LineRenderer>();

        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            Vector3 origin = originLaser.position;
            Vector3 direction = originLaser.forward;
            Vector3 target;

            // Raycast desde el arma hacia adelante
            if (Physics.Raycast(origin, direction, out RaycastHit hit, maxDistance, hitLayers))
            {
                target = hit.point;
            }
            else
            {
                target = origin + direction * maxDistance;
            }

            // Dibujar láser
            if (!line.enabled)
                line.enabled = true;

            line.SetPosition(0, origin);
            line.SetPosition(1, target);

            // (Opcional) Rotar el objeto si querés que mire al punto de impacto
            // transform.LookAt(target);
        }
        else
        {
            line.enabled = false;
        }
    }

}
