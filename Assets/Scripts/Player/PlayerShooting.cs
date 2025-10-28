using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shooting Properties")]
    [Tooltip("The bullet prefab to instantiate.")]
    [SerializeField] GameObject bulletPrefab;

    [Tooltip("The point from which the bullet will be spawned (e.g., a gun muzzle).")]
    [SerializeField] Transform bulletSpawnPoint;
    
    GameObject aimIndicator;
    Bullet bulletComponent;


    void Start()
    {
        aimIndicator = GameObject.FindGameObjectWithTag("Aim");
        if(aimIndicator == null)
        {
            Debug.LogWarning("Target Aim object not found");
        }
    }

    private void Update()
    {
        // Check for the fire button (e.g., left mouse button).
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        // Check if the aim indicator is active. We only want to shoot if we have a valid aim point.
        var quantityAmmo = ResourceManager.Instance.GetResourceQuantity(ResourceType.Amunitio);

        if (quantityAmmo <= 0) 
        {
            Debug.Log("No ammo");
            AudioManager.Instance.Play("NoAmmo");
            return;
        }

        //if (aimIndicator.activeInHierarchy)
        //{
            GameObject bulletInstance = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation) as GameObject;
            ResourceManager.Instance.AddResources(name: ResourceType.Amunitio.ToString(), qty: -1);
            Vector3 direction = (aimIndicator.transform.position - bulletSpawnPoint.position).normalized;
            AudioManager.Instance.Play("Shoot");
            //ResourceManager.Instance
            bulletComponent = bulletInstance.GetComponent<Bullet>();
            
            if (bulletComponent != null)
            {
                //bulletComponent.SetDirection(direction);
            }
        //}         
    }
}
