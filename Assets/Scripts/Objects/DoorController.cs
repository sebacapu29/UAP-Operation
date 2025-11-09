using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator doorAnimator;

    public void OpenDoor()
    {
        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("Open");
        }
        else
        {
            // Simple movimiento de levantar puerta
            transform.position += new Vector3(0f, 2f, 0f);
        }
    }
}
