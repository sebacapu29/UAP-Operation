using UnityEngine;

public class MultiDepositController : MonoBehaviour
{
    public DepositSlot[] requiredSlots;
    public DoorController doorToOpen;

    private int slotsFilled = 0;

    public void NotifySlotFilled(DepositSlot slot)
    {
        slotsFilled++;

        if (slotsFilled >= requiredSlots.Length)
        {
            if (doorToOpen != null)
                doorToOpen.OpenDoor();
        }
    }
}
