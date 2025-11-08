using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardKey : MonoBehaviour
{

  
    [Header("Configuración de tarjeta")]
    [Tooltip("Valor de puntos que otorga est tarjeta")]
    [SerializeField] private int _pointValue = 1;
    
    [Header("Efectos Opcionales")]
    [Tooltip("Efecto de partículas al ser recolectado")]
    [SerializeField] private ParticleSystem _collectEffect;
    
    [Tooltip("Sonido al ser recolectado")]
    [SerializeField] private AudioClip _collectSound;

    private bool _isCollected = false;

    // Se llama cuando el jugador toca la tarjeta
    private void OnTriggerEnter(Collider other)
    {
        // Verificar que sea el jugador y que no haya sido ya recolectado
        if (other.CompareTag("Player") && !_isCollected)
        {
            _isCollected = true;
            CollectCard();
        }
    }

    private void CollectCard()
    {
        // Notificar al Manager que se recolectó una tarjeta
        CardKeyManager.Instance?.AddCard(_pointValue);
        
        // Reproducir efectos (si existen)
        PlayCollectionEffects();
        
        // Desactivar o destruir la tarjeta
        DisappearCard();
    }

    private void PlayCollectionEffects()
    {
        // Efecto de partículas
        if (_collectEffect != null)
        {
            ParticleSystem effect = Instantiate(_collectEffect, transform.position, Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, effect.main.duration);
        }
        
        // Sonido
        if (_collectSound != null)
        {
            AudioSource.PlayClipAtPoint(_collectSound, transform.position);
        }
    }

    private void DisappearCard()
    {
        // Desactivar el renderer y collider inmediatamente
        GetComponent<Renderer>().enabled = false;
        Collider collider = GetComponent<Collider>();
        if (collider != null) collider.enabled = false;
        
        // Destruir el objeto después de un pequeño delay para que los efectos se reproduzcan
        Destroy(gameObject, 0.1f);
    }

}
