using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RaycastAim : MonoBehaviour
{
    [Header("Raycast Properties")]
    [Tooltip("The maximum distance the raycast can travel.")]
    [SerializeField] float maxDistance = 50f;

    [Tooltip("LayerMask to control which layers the raycast will hit.")]
    [SerializeField] LayerMask hitLayers;

    [Header("Visuals")]
    [Tooltip("The GameObject to place at the raycast hit point as a visual indicator.")]
    [SerializeField] GameObject aimIndicator;

    [Tooltip("The amount to raise the aim indicator above the hit point to ensure visibility.")]
    [SerializeField] float surfaceOffset = 0.1f;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;

        if (aimIndicator == null)
        {
            Debug.LogError("Please assign a GameObject in the Inspector.");
        }
        else
        {
            //aimIndicator.SetActive(false);
        }
    }

    private void Update()
    {

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (Input.GetButton("Fire2"))
        {
            if (Physics.Raycast(ray, out hit, maxDistance, hitLayers))
            {
                // The ray hit something. Position the aim indicator at the hit point.
                // Add a small vertical offset to prevent it from clipping into the floor or objects.
                // Posicionar
                Vector3 indicatorPosition = hit.point + hit.normal * surfaceOffset;
                aimIndicator.transform.position = indicatorPosition;

                // Orientar hacia la cámara
                aimIndicator.transform.LookAt(mainCamera.transform);

                // Escalar para que se vea siempre igual
                float distance = Vector3.Distance(mainCamera.transform.position, indicatorPosition);
                float scaleFactor = distance * 0.0003f; // ajusta el 0.02f según el tamaño deseado
                aimIndicator.transform.localScale = Vector3.one * scaleFactor;


                // Activate the indicator if it's not already active.
                //if (!aimIndicator.activeInHierarchy)
                //{
                //    aimIndicator.SetActive(true);
                //}
            }
            else
            {
                //  The ray did not hit anything.
                //Hide the aim indicator.
                //if (aimIndicator.activeInHierarchy)
                //{
                //    aimIndicator.SetActive(false);
                //}
            }
        }
        else
        {

            aimIndicator.SetActive(false);
        }
    }
}
