using UnityEngine;

public class HandleCheckPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        var level = scene.Split('_');
        if (level[0] != "Level") return;
        if(level.Length < 2) return;

        if(level[1] == "2")
        {
            var qtyCard = ResourceManager.Instance.GetResourceQuantity(ResourceType.Card);
            
            if (collision.CompareTag("Player") && qtyCard>=1)
            {
                // Assuming Level_1_Manager has a method to load the next level
                //Corregir esta mal el level manager
                LevelManager.Instance.LoadNextLevel();
            }
        }
        else
        {
            if (collision.CompareTag("Player"))
            {
                // Assuming Level_1_Manager has a method to load the next level
                LevelManager.Instance.LoadNextLevel();
            }
        }

       
    }
}
