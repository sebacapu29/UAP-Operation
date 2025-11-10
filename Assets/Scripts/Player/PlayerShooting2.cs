using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting2 : MonoBehaviour
{
  [Header("Shooting Properties")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawnPoint;
    
    private GameObject aimIndicator;
    private BulletController bulletController; // ← Cambiar a BulletController

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
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
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
        
        // Obtener BulletController en lugar de Bullet
        bulletController = bulletInstance.GetComponent<BulletController>();
        
        if (bulletController != null)
        {
            // Disparar en la dirección que MIRA el jugador
            Vector3 shootDirection = transform.forward;
            bulletController.SetDirection(shootDirection);
        }
    }
}
