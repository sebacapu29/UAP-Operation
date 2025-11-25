
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIOnGUINew : MonoBehaviour
{
    public static UIOnGUINew Instance;

    [Header("Referencias UI")]
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI grenadeText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void UpdateCollectedItems(string resourceName, int qty)
    {
        switch (resourceName)
        {
            case "Health":
                healthText.text = qty.ToString();
                break;
            case "Amunitio":
                ammoText.text = qty.ToString();
                break;
            case "Grenade":
                grenadeText.text = qty.ToString();
                break;
        }
    }
}
