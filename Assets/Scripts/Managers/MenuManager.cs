// using UnityEngine;
// using UnityEngine.InputSystem;
// using UnityEngine.SceneManagement;

// public class MenuController : MonoBehaviour
// {
//     public InputAction pauseAction;
//     //[SerializeField] GameObject menuPause;
//     public static MenuController Instance;
//     public bool pauseIsPressed = false;

//     void OnEnable() => pauseAction.Enable();
//     void OnDisable() => pauseAction.Disable();

//     private void Awake()
//     {
//         if (Instance != null && Instance != this)
//         {
//             Destroy(gameObject);
//             return;
//         }
//         Instance = this;
//     }
//     // Update is called once per frame
//     void Update()
//     {
//         MenuActions();
//     }
//     void MenuActions()
//     {
//         var levelName = SceneManager.GetActiveScene().name;
        
//         if (pauseAction.triggered)
//         {
//             pauseIsPressed = !pauseIsPressed;
            
//             if(pauseIsPressed)
//                 Debug.Log("Menu Paused");

//             Time.timeScale = pauseIsPressed ? 0f : 1f;
//             if (pauseIsPressed) { AudioManager.Instance.StopAll(); 
//             }
//             else
//             {
//                 AudioManager.Instance.Play("Ambience_Level1");
//             }
//             //menuPause.SetActive(pauseIsPressed);
//         }
//     }

//     public void NewGame()
//     {       
//         SceneManager.LoadScene("Level_1");
//     }
// }
