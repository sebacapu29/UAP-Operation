using System.Collections.Generic;
using UnityEngine;

public class SirenBoxController : MonoBehaviour
{
private KeyCode pickupKey = KeyCode.E;
private float closestDist = 3f;
private List<Light> _alertLight = new List<Light>();
private Transform player;


    void Awake()
    {

        var objsSirenLights = GameObject.FindGameObjectsWithTag("SirenLight");
        foreach (var obj in objsSirenLights)
        {
            var sirenLight = obj.GetComponents<SirenLight>();

            if (sirenLight != null && sirenLight.Length > 0)
            { 
                foreach (var light in sirenLight)
                {
                    _alertLight.Add(light.sirenLight);
                }
            }
        }

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if (Input.GetKeyDown(pickupKey))
        {
            float dist = Vector3.Distance(transform.position, player.transform.position);
            if (dist <= closestDist)
            {
                TurnOffLight();                
            }            
        }
    }
    void TurnOffLight()
    {
        foreach (var light in _alertLight)
        {                  
            light.enabled = false;
        }
        AudioManager.Instance.Play("SwitchButton"); 
        AudioManager.Instance.Stop("IntruderAlert");
    }
}
