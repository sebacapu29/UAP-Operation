using UnityEngine;

public class PickupObject : MonoBehaviour
{
    [Header("Identification")]
    public string itemID;

    private bool isBeingCarried = false;
    private bool isDeposited = false;

    public void PickUp(Transform player, Transform handPosition, Transform pickupOffset)
    {
        isBeingCarried = true;
        transform.position = handPosition.position - (pickupOffset.position - transform.position);
        transform.parent = player;
        transform.position = handPosition.position;
        transform.rotation = handPosition.rotation;
    }

    public void ForceDrop()
    {
        isBeingCarried = false;
        transform.parent = null;
    }

    public void MarkAsDeposited()
    {
        isDeposited = true;
    }

    public bool IsBeingCarried()
    {
        return isBeingCarried;
    }

    public bool IsDeposited()
    {
        return isDeposited;
    }

    public string GetItemID()
    {
        return itemID;
    }
}
