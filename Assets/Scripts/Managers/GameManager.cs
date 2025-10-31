using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] GameObject gameOverMenu;
    public bool isGameOver = false;
    public bool isMissionComplete = false;
    public int currentPlayerHealth;
    private PlayerHealth playerHealth;
    public TextMeshProUGUI title;
    public Button btnPlay;

    private void Awake()
    {
        transform.SetParent(null);
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
       
        // Cursor.lockState = CursorLockMode.Confined;
        // Cursor.visible = false;
        var currentScene = SceneManager.GetActiveScene().name;
        if (currentScene != "MainMenu")
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                playerHealth = player.GetComponent<PlayerHealth>();

            // btnPlay.onClick.AddListener(() => LoadScene());
        }
    }
    void LoadScene()
    {
        if (isGameOver)
            RetryLevel();
        else
            RestartGame();
    }
    // Update is called once per frame
    void Update()
    {
        if (playerHealth != null)
            currentPlayerHealth = playerHealth.Health;

        if (isGameOver || isMissionComplete)
        {
            // title.text = isGameOver ? "Game Over" : "Mission Complete!";
            // gameOverMenu.SetActive(true);
            // AudioManager.Instance.Stop("Ambience_Level1");
            // //AudioManager.Instance.Play("GameOver");
            // Time.timeScale = 0f; // Pause the game
            // Cursor.visible = true;
        }

    }
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume the game
        isGameOver = false;
        isMissionComplete = false;
        SceneManager.LoadScene("Level_1");
    }
    public void RetryLevel()
    {
        Time.timeScale = 1f; // Resume the game
        isGameOver = false;
        isMissionComplete = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void OnApplicationFocus(bool hasFocus)
    {
        // if (!hasFocus)
        // {
        //     Cursor.visible = true;
        //     Cursor.lockState = CursorLockMode.None;
        // }
        // else
        // {
        //     Cursor.visible = false;
        //     Cursor.lockState = CursorLockMode.Confined;
        // }
    }

}
