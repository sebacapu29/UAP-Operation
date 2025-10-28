using UnityEngine;

public class MiraController : MonoBehaviour
{
    Camera mainCamera;
    GameObject enemy;

    public GameObject Enemy{set { enemy = value;}}

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;    
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy == null) return;
        
        if(enemy.gameObject.CompareTag("Enemy"))
        {
            
            transform.LookAt(mainCamera.transform.position + transform.up);
        }
        else
        {
            transform.position = transform.up;
        }
    }
}
