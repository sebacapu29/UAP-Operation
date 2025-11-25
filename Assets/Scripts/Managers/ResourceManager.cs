using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;
    [SerializeField] UIOnGUI gui;
    public static Dictionary<string, int> playerInventory = new Dictionary<string, int>();
    private void Awake()
    {
        transform.SetParent(null);
        // If an instance already exists and it's not this one, destroy this new instance
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            // Otherwise, set this instance as the singleton
            Instance = this;
            // Optionally, prevent this GameObject from being destroyed when loading new scenes
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        AddResources("Amunitio", 4);
    }

    public void AddResources(string name, int qty)
    {
        if (!playerInventory.ContainsKey(name))
        {
            playerInventory.Add(name, qty);
        }
        else
        {
            playerInventory[name] += qty;
        }

        UIOnGUI.Instance.UpdateCollectedItems(name, playerInventory[name]);
    }

    internal int GetResourceQuantity(ResourceType ammo)
    {
        if (playerInventory.TryGetValue(ammo.ToString(), out int qty))
        {
            return qty;
        }
        else
        {
            return 0;
        }
    }
    // internal ResourceType GetResource(string resourceName)
    // {
    //     if (Enum.TryParse(resourceName, out ResourceType resourceType))
    //     {
    //         return resourceType;
    //     }
    //     else
    //     {
    //         Debug.LogError($"Resource type '{resourceName}' is not defined.");
    //     }
    //     return ResourceType.Amunitio; // Default return value, adjust as needed
    // }
}
public enum ResourceType
{
    Amunitio,
    Health,
    Mana,
    Card
}

