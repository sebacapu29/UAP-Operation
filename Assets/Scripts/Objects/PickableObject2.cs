
using UnityEngine;

public class PickableObj : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if(other.CompareTag("Player"))
        {
            // Notify the UI to update the collected items text
            UIOnGUI.Instance.ShowMessage("Presiona 'E' para recolectar " + gameObject.name);
        }
    }
}
