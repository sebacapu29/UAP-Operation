using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shooting Properties")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawnPoint;

    [Header("Grenade Properties")]
    [SerializeField] GameObject grenadePrefab;
    [SerializeField] float throwForce = 10f;

    GameObject aimIndicator;

    void Start()
    {
        aimIndicator = GameObject.FindGameObjectWithTag("Aim");
        if (aimIndicator == null)
        {
            Debug.LogWarning("Target Aim object not found");
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            ThrowGrenade();
        }
    }

    private void Shoot()
    {
        var quantityAmmo = ResourceManager.Instance.GetResourceQuantity(ResourceType.Amunitio);
        if (quantityAmmo <= 0)
        {
            Debug.Log("No ammo");
            AudioManager.Instance.Play("NoAmmo");
            return;
        }

        GameObject bulletInstance = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        ResourceManager.Instance.AddResources(ResourceType.Amunitio.ToString(), -1);
        AudioManager.Instance.Play("Shoot");
    }

 private void ThrowGrenade()
{
       var quantityGrenade = ResourceManager.Instance.GetResourceQuantity(ResourceType.Grenade);

        if (quantityGrenade <= 0)
        {
            Debug.Log("No Grenade");
            AudioManager.Instance.Play("NoAmmo");
            return;
        }
    GameObject grenadeInstance = Instantiate(grenadePrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    ResourceManager.Instance.AddResources(ResourceType.Grenade.ToString(), -1);
    Rigidbody rb = grenadeInstance.GetComponent<Rigidbody>();

    if (rb != null)
    {
        // Dirección hacia adelante con un pequeño arco
        Vector3 throwDirection = bulletSpawnPoint.forward + Vector3.up * 0.5f;
        rb.AddForce(throwDirection.normalized * throwForce, ForceMode.VelocityChange);
    }

    AudioManager.Instance.Play("ThrowGrenade");
}

}
