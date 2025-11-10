using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntoARespawn : MonoBehaviour
{
    [Header("Configuración del Punto de Respawn")]
    [SerializeField] private ParticleSystem _respawnParticles;
    [SerializeField] private AudioClip _respawnSound;
    
    void Start()
    {
        
        if (!gameObject.CompareTag("Respawn"))
        {
            Debug.LogWarning($"El objeto {gameObject.name} debería tener tag 'Respawn' para funcionar como punto de respawn");
        }
        
        //agregar efectos visuales si no existen
        if (_respawnParticles == null)
            _respawnParticles = GetComponentInChildren<ParticleSystem>();
    }
    
    public Vector3 GetRespawnPosition()
    {
        return transform.position;
    }
    
    // Para activar efectos cuando el jugador respawnea
    public void PlayRespawnEffects()
    {
        if (_respawnParticles != null)
            _respawnParticles.Play();
            
        
    }
}