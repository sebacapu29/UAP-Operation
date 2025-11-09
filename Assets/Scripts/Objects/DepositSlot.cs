using UnityEngine;

public class DepositSlot : MonoBehaviour
{
    public Transform depositPosition; // Punto donde se deposita
    public string expectedItemID;     // ID que debe coincidir
    public MultiDepositController depositController;

    private bool isOccupied = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isOccupied) return;

        PlayerPickupController playerController = other.GetComponent<PlayerPickupController>();
        if (playerController == null) return;

        PickupObject carriedObject = playerController.GetCarriedObject();
        if (carriedObject == null) return;

        if (!carriedObject.IsDeposited() && carriedObject.GetItemID() == expectedItemID)
        {
            // Deposito el object
            carriedObject.ForceDrop();
            playerController.ClearCarriedObject();

            carriedObject.transform.position = depositPosition.position;
            carriedObject.transform.rotation = depositPosition.rotation;
            carriedObject.transform.parent = transform;
            carriedObject.MarkAsDeposited();

            isOccupied = true;

            // Da aviso a DoorController
            if (depositController != null)
                depositController.NotifySlotFilled(this);
        }
    }


    public bool IsOccupied()
    {
        return isOccupied;
    }
}
