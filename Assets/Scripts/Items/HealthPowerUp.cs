using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{
   // Cantidad de salud que este power-up añadirá al jugador
    [SerializeField]
    private int healthAmount = 25; 

    private void OnTriggerEnter(Collider other)
    {
        // 1. Verificar si el objeto que ha entrado en el trigger es el jugador
        // Asumiendo que el GameObject del jugador tiene la etiqueta "Player"
        if (other.CompareTag("Player")) 
        {
            // 2. Intentar obtener el componente PlayerHealth del objeto colisionado
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                // 3. Obtener el HealthManager para acceder a la salud actual y el método de curación
                // Como PlayerHealth hereda de HealthManager, podemos usarlo para la curación.
                HealthManager healthManager = playerHealth.GetComponent<HealthManager>();

                if (healthManager != null)
                {
                    // 4. Calcular la nueva salud (usando un método de curación si existiera,
                    // pero para simplificar, usaremos un método que agregaremos a HealthManager)
                    
                    // Llama a la función para curar al jugador
                    healthManager.Heal(healthAmount); 
                    
                    // Muestra un mensaje de que el power-up se usó
                    Debug.Log($"El jugador recogió el power-up de salud. Salud aumentada en {healthAmount}.");

                    // 5. Destruir el power-up después de que el jugador lo recoja
                    Destroy(gameObject); 
                }
                else
                {
                    Debug.LogError("El objeto 'Player' no tiene un componente HealthManager.");
                }
            }
        }
    }
}
