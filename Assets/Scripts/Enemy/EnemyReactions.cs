using UnityEngine;

public class EnemyReactions : MonoBehaviour
{

    [SerializeField] internal GameObject exclamationIcon;
    EnemyHealth enemyHealth;
    private bool isEnemyChaser = false;
   void Awake()
    {
        isEnemyChaser = gameObject.tag == "EnemyChaser";
        exclamationIcon.SetActive(false);
        enemyHealth = GetComponent<EnemyHealth>();

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !enemyHealth.isEnemySleeped)
        {
            exclamationIcon.SetActive(true);
        }
    }
         void OnTriggerExit(Collider other)
   {
        if(other.CompareTag("Player")) 
        {
             exclamationIcon.SetActive(false);
        }
   }
}
