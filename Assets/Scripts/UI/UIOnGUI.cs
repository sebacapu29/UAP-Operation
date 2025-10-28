using UnityEngine;

public class UIOnGUI : MonoBehaviour
{
    public string collectedItemsText = "Esperando la primer recolección...";
    public static UIOnGUI Instance { get; private set; }

    private GUIStyle labelStyle;
    private GUIStyle labelLifeStyle;
    //private GUIStyle buttonStyle;

    private void Awake()
    {
        // If an instance already exists and it's not this one, destroy this new instance
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            // Otherwise, set this instance as the singleton
            Instance = this;
            // Optionally, prevent this GameObject from being destroyed when loading new scenes
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        labelStyle = new GUIStyle();
        labelLifeStyle = new GUIStyle();

        labelLifeStyle.fontSize = 30;
        labelLifeStyle.normal.textColor = Color.red;
        labelLifeStyle.alignment = TextAnchor.UpperLeft;
        labelLifeStyle.fontStyle = FontStyle.Bold;

        labelStyle.fontSize = 30;
        labelStyle.normal.textColor = Color.white;
        labelStyle.alignment = TextAnchor.MiddleCenter;
        //buttonStyle = new GUIStyle("Button");
        //buttonStyle.fontSize = 18;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateCollectedItems(string name, int quantity)
    {
        collectedItemsText = "Objeto recolectado " + name + "/" + quantity;
        Debug.Log("UI Actualizada: " + collectedItemsText);
    }
    private void OnGUI()
    {
        // Display the collected items text at the top center of the screen
        Rect labelRect = new Rect(Screen.width / 2 - 150, 10, 300, 30);
        GUI.Label(labelRect, collectedItemsText, labelStyle);
        GUI.Label(new Rect(10, 10, 200, 30), "Vida: "+ GameManager.Instance.currentPlayerHealth , labelLifeStyle);
        // Create a button to simulate collecting an item
        Rect buttonRect = new Rect(Screen.width / 2 - 75, 50, 150, 40);
        //if (GUI.Button(buttonRect, "Reiniciar", buttonStyle))
        //{
        //    // Simulate collecting an item (for testing purposes)
        //}
    }
}
