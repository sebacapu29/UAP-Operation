
using UnityEngine;

public class PickableObj : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            // Notify the UI to update the collected items text
            UIOnGUINew.Instance.ShowTutorialMessage("Press 'E' to pick up the item.");
        }
    }
    void OnTriggerExit(Collider other)
    {
        UIOnGUINew.Instance.ShowTutorialMessage("");
    }
}
