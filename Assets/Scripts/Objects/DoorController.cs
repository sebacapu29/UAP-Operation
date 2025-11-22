using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator doorAnimator;
    public Vector3 movementDirection = new Vector3(2.5f, 0f, 0f);
    public void OpenDoor()
    {
        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("Open");
        }
        else
        {
            // Simple movimiento de levantar puerta
            transform.position += movementDirection;
        }
        AudioManager.Instance.Play("DoorOpen");
    }
}
