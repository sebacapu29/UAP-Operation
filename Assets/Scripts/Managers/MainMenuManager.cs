using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
 
    // Update is called once per frame

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void PlayNewGame()
    {
        SceneManager.LoadScene("Level1");
    }
}
