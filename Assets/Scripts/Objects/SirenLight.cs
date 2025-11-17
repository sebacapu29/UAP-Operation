using UnityEngine;

public class SirenLight : MonoBehaviour
{
    public Light sirenLight;
    public Color color1 = Color.red;
    public Color color2 = Color.blue;
    public float flashSpeed = 1.5f;
    private float rotationSpeed = 65f;

    void Awake()
    {
        sirenLight.enabled = false;
    }
    void Update()
    {
        if(sirenLight.enabled)
        {
            LightFlash();
        }
    }
    // public void TurnOnLight()
    // {
    //     Debug.Log("Siren Light On");
    //     sirenLight.enabled = true;
    // }
    void LightFlash()
    {
         float t = Mathf.PingPong(Time.time * flashSpeed, 1f);
        sirenLight.color = Color.Lerp(color1, color2, t);
        sirenLight.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}