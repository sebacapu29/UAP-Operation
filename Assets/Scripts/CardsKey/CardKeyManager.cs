using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardKeyManager : MonoBehaviour
{
     [Header("Configuración de Tarjetas")]
    [Tooltip("Cantidad total de Tarjetas requeridas para activar el punto B")]
    [SerializeField] private int _CardsRequired = 2;
    
    [Header("Referencias")]
    [Tooltip("Objeto Punto B que se activará cuando se recolecten todas las tarjetas")]
    [SerializeField] private GameObject _pointB;

    // Singleton para fácil acceso
    public static CardKeyManager Instance { get; private set; }

    // Propiedades públicas para acceso externo
    public int CurrentCards { get; private set; }
    public int CardsRequired => _CardsRequired;
    public bool AllCardsCollected => CurrentCards >= _CardsRequired;

    // Eventos para UI u otros sistemas
    public System.Action<int> OnCardCollected;
    public System.Action OnAllCardsCollected;

    private void Awake()
    {
        // Configurar Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitializeManager();
    }

    private void InitializeManager()
    {
        CurrentCards = 0;
        
        // Ocultar el Punto B al inicio
        if (_pointB != null)
        {
            _pointB.SetActive(false);
        }
    }

    /// <summary>
    /// Añade tarjetas al contador y verifica si se completó la colección
    /// </summary>
    public void AddCard(int value = 1)
    {
        CurrentCards += value;
        Debug.Log($"Tarjeta recolectado! Total: {CurrentCards}/{_CardsRequired}");

        // Disparar evento de tarjeta recolectado
        OnCardCollected?.Invoke(CurrentCards);

        // Verificar si se recolectaron todas las tarjetas
        if (AllCardsCollected)
        {
            ActivatePointB();
        }
    }

    private void ActivatePointB()
    {
        Debug.Log("¡Todas las tarjetas recolectados! Activando Punto B...");
        
        if (_pointB != null)
        {
            _pointB.SetActive(true);
            OnAllCardsCollected?.Invoke();
        }
        else
        {
            Debug.LogError("No se puede activar el Punto B - referencia nula.");
        }
    }

    /// <summary>
    /// Método para debug - fuerza la activación del Punto B
    /// </summary>
    public void DebugActivatePointB()
    {
        CurrentCards = _CardsRequired;
        ActivatePointB();
    }

    /// <summary>
    /// Reinicia el contador de tarjetas (útil para reiniciar nivel)
    /// </summary>
    public void ResetCards()
    {
        CurrentCards = 0;
        OnCardCollected?.Invoke(CurrentCards);
    }
}
