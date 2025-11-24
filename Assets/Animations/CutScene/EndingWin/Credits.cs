using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsEnd : MonoBehaviour
{
    public string sceneToLoad = "MainMenu";

    public void LoadNextScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
