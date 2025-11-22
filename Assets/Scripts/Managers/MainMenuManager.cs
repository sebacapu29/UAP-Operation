using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] Button buttonNewGame;
    [SerializeField] Button buttonQuitGame;
    AsyncOperation asyncLoad;

    void Start()
    {
        buttonNewGame.onClick.AddListener(ActivateScene);
        buttonQuitGame.onClick.AddListener(QuitGame);
        StartCoroutine(PreloadScene("Level_1"));
    }

    IEnumerator PreloadScene(string sceneName)
    {
        asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    void ActivateScene()
    {
        asyncLoad.allowSceneActivation = true;
    }
   
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
