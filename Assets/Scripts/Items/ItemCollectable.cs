using UnityEngine;

public class ItemCollectable : MonoBehaviour
{
    [Tooltip("The name of the item to be collected")]
    [SerializeField] string itemName;
    [Tooltip("The quantity of the item to be collected")]
    [SerializeField] int itemQuantity = 1;

    public string ItemName { get { return itemName; } }
    public int ItemQty { get { return itemQuantity; } }


}
