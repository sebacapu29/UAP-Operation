using UnityEngine;

public class PlayerCollect : MonoBehaviour
{
    [SerializeField] GameObject resources;
    ItemCollectable itemCollectable;
    ResourceManager resourceManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        resourceManager = ResourceManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            itemCollectable = other.GetComponent<ItemCollectable>();
            if (itemCollectable != null)
            {
                string name = itemCollectable.ItemName;
                int amount = itemCollectable.ItemQty;
                //Si name es Amunition reproducir sonido de recarga
                if(name == ResourceType.Amunitio.ToString())
                {
                    AudioManager.Instance.Play("Reload");
                }
                else
                {
                    AudioManager.Instance.Play("GrabItem");
                }
                    resourceManager.AddResources(name, amount);
                Destroy(other.gameObject);
            }
        }
        
    }
}
