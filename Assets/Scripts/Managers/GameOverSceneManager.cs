using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameOverSceneManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector playableDirector;
    private bool sceneLoaded = false;

    void Start()
    {
        if (playableDirector == null)
            playableDirector = GetComponent<PlayableDirector>() ?? GetComponentInChildren<PlayableDirector>();

        if (playableDirector != null)
            playableDirector.stopped += OnPlayableStopped;
    }

    private void OnDestroy()
    {
        if (playableDirector != null)
            playableDirector.stopped -= OnPlayableStopped;
    }

    private void OnPlayableStopped(PlayableDirector director)
    {
        if (sceneLoaded) return;
        sceneLoaded = true;
        GameManager.Instance.isGameOver = false;
        AudioManager.Instance.Play("Ambience_Level_1");
        // Usar la escena anterior registrada por PreviousSceneTracker
        string previous = PreviousSceneTracker.LastSceneName;
        if (string.IsNullOrEmpty(previous))
        {
            // Fallback seguro
            SceneManager.LoadScene("Level_1");
            return;
        }

        // Si tenemos un nombre válido, recargar/volver a esa escena
        SceneManager.LoadScene(previous);
    }
}

// Clase auxiliar que guarda la escena anterior. Se registra al iniciar la app.
static class PreviousSceneTracker
{
    public static string LastSceneName { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Init()
    {
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    static void OnActiveSceneChanged(Scene oldScene, Scene newScene)
    {
        if (!string.IsNullOrEmpty(oldScene.name))
            LastSceneName = oldScene.name;
    }
}