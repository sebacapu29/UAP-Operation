using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControllerV2 : MonoBehaviour
{
    public Animator doorAnimator;
    
    [Header("Configuración de Movimiento Manual")]
    public Vector3 movementDirection = new Vector3(2.5f, 0f, 0f);
    public bool useManualMovement = false;
    
    [Header("Opciones de Movimiento Predefinido")]
    public MovementType movementType = MovementType.Sideways;
    
    public enum MovementType
    {
        Sideways,    // Movimiento lateral
        Upwards,     // Movimiento hacia arriba
        Downwards,   // Movimiento hacia abajo
        Custom       // Movimiento personalizado
    }

    private void Start()
    {
        // Configurar dirección predefinida según el tipo seleccionado
        if (!useManualMovement)
        {
            switch (movementType)
            {
                case MovementType.Sideways:
                    movementDirection = new Vector3(2.5f, 0f, 0f);
                    break;
                case MovementType.Upwards:
                    movementDirection = new Vector3(0f, 2.5f, 0f);
                    break;
                case MovementType.Downwards:
                    movementDirection = new Vector3(0f, -2.5f, 0f);
                    break;
                case MovementType.Custom:
                    // Usa el valor personalizado que hayas puesto en movementDirection
                    break;
            }
        }
    }

    public void OpenDoor()
    {
        if (doorAnimator != null && !useManualMovement)
        {
            doorAnimator.SetTrigger("Open");
        }
        else
        {
            // Movimiento manual según la configuración
            transform.position += movementDirection;
        }
        
        AudioManager.Instance.Play("DoorOpen");
    }
}
