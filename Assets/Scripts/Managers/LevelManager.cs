using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public bool disableAmbienceMusic = false;
    public bool isPlayerFinded = false;
    public bool activateWaveEnemy = false;
    
    [SerializeField] Texture2D cursorTexture; 
    [SerializeField] Vector2 hotSpot = Vector2.zero; 
    private CursorMode cursorMode = CursorMode.Auto;


    private void Start()
    {
        if (!disableAmbienceMusic)
            AudioManager.Instance.Play("Ambience_Level1");

        ResourceManager.Instance.AddResources("Amunitio", 5);
        
        // Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
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

    public void LoadNextLevel()
    {
        
        var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        var level = scene.Split('_');
        var currentLevel = int.Parse(level[1]);
        if (currentLevel >= 3) {
            GameManager.Instance.isMissionComplete = true;
            return;
        }

        var nextLevel = currentLevel + 1;   
        GameManager.Instance.LoadLevel("Level_"+ nextLevel); 
    }
}
