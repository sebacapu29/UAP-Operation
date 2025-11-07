using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDepositArea : MonoBehaviour
{
    public string tagMovableObject = "MovableObject"; // El objeto a depositar debe tener este tag
    public Transform depositPosition; // Transform de referencia para posicionar el objeto

    private void OnTriggerEnter(Collider other)
    {
        // Controlo que el objeto entrante es el Player
        if (other.CompareTag("Player"))
        {
            // Revisa si el Player carga algún objeto
            ObjectPickUp objetoPickUp = FindObjectOfType<ObjectPickUp>();
            if (objetoPickUp != null && objetoPickUp.IsPicking())
            {
                // Obtengo el objeto se está cargando
                GameObject objeto = objetoPickUp.gameObject;

                // Se suelta y posiciona en el depósito
                objetoPickUp.Drop();
                objeto.transform.position = depositPosition.position;
                objeto.transform.rotation = depositPosition.rotation;
                
                // Marcar como depositado
                objetoPickUp.IsDeposited();

                // Se fija al depósito
                objeto.transform.parent = this.transform;
            }
        }
    }
}
