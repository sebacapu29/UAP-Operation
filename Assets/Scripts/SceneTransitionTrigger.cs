using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionTrigger : MonoBehaviour
{
    [Header("Configuración de Transición")]
    [Tooltip("Nombre de la escena a la que se cambiará.")]
    [SerializeField] private string _sceneToLoad = "StartScene";

    [Tooltip("Tiempo de espera en segundos antes de cargar la nueva escena.")]
    [SerializeField] private float _delayBeforeLoad = 3.0f;

    [Tooltip("Etiqueta (Tag) del objeto que puede activar la transición (normalmente 'Player').")]
    [SerializeField] private string _playerTag = "Player";

    private bool _isPlayerInside = false;
    private bool _isTransitioning = false;

    // Se llama cuando otro collider entra en este trigger
    private void OnTriggerEnter(Collider other)
    {
        // 1. Verificar la etiqueta del jugador y que no esté ya en transición
        if (other.CompareTag(_playerTag) && !_isTransitioning)
        {
            _isPlayerInside = true;
            _isTransitioning = true;
            Debug.Log($"Jugador ha entrado en el punto de transición. Cargando '{_sceneToLoad}' en {_delayBeforeLoad} segundos...");
            
            // 2. Inicia la corrutina para esperar y cargar la escena
            StartCoroutine(LoadSceneAfterDelay());
        }
    }

    // Se llama cuando otro collider sale de este trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_playerTag) && _isPlayerInside)
        {
            _isPlayerInside = false;
            // Si el jugador sale antes de la carga, cancelamos la transición
            if (_isTransitioning)
            {
                StopAllCoroutines();
                _isTransitioning = false;
                Debug.Log("Jugador ha salido del punto de transición. Transición cancelada.");
            }
        }
    }

    /// <summary>
    /// Corrutina que espera el tiempo definido y luego carga la escena.
    /// </summary>
    private IEnumerator LoadSceneAfterDelay()
    {
        // Espera la cantidad de segundos especificada
        yield return new WaitForSeconds(_delayBeforeLoad);

        // Solo carga la escena si el jugador sigue dentro del trigger
        if (_isPlayerInside)
        {
            Debug.Log($"Cargando escena: {_sceneToLoad}");
            
            try
            {
                // Esta línea ahora debería funcionar gracias a 'using UnityEngine.SceneManagement;'
                SceneManager.LoadScene(_sceneToLoad); 
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error al cargar la escena '{_sceneToLoad}'. Asegúrate de que el nombre sea correcto y esté en File -> Build Settings.\nDetalles: {e.Message}");
                _isTransitioning = false;
            }
        }
        else
        {
            _isTransitioning = false;
        }
    }

    /// <summary>
    /// Permite cambiar la escena de destino en tiempo de ejecución.
    /// Esto es lo que necesitas si quieres cambiar a otra escena en el futuro.
    /// </summary>
    public void SetSceneToLoad(string newSceneName)
    {
        _sceneToLoad = newSceneName;
        Debug.Log($"Escena de destino cambiada a: {newSceneName}");
    }
}