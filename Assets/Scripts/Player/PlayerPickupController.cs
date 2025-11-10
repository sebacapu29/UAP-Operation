using UnityEngine;

public class PlayerPickupController : MonoBehaviour
{
    [Header("Pickup Settings")]
    public float pickupRange = 3f;
    public KeyCode pickupKey = KeyCode.E;
    public Transform handPosition;
    public Transform pickupOffset; // en caso de otro objeto

    private PickupObject carriedObject;

    void Update()
    {
        if (Input.GetKeyDown(pickupKey))
        {
            if (carriedObject == null)
                TryPickUp();
            else
                Drop();
        }
    }

    void TryPickUp()
    {
        PickupObject nearest = FindClosestPickup();

        if (nearest != null && !nearest.IsBeingCarried() && !nearest.IsDeposited())
        {
            carriedObject = nearest;
            carriedObject.PickUp(transform, handPosition, pickupOffset);
        }
    }

    void Drop()
    {
        if (carriedObject != null)
        {
            carriedObject.ForceDrop();
            carriedObject = null;
        }
    }

    PickupObject FindClosestPickup()
    {
        PickupObject[] allObjects = FindObjectsOfType<PickupObject>();
        PickupObject closest = null;
        float closestDist = pickupRange;

        foreach (PickupObject obj in allObjects)
        {
            float dist = Vector3.Distance(transform.position, obj.transform.position);
            if (dist <= closestDist && !obj.IsDeposited())
            {
                closest = obj;
                closestDist = dist;
            }
        }

        return closest;
    }

    public PickupObject GetCarriedObject()
    {
        return carriedObject;
    }
    public void ClearCarriedObject()
    {
        carriedObject = null;
    }


}
