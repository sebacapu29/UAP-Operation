using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAINavigation : MonoBehaviour
{
    
    [SerializeField] float _porsuitSpeed = 3.0f;

    GameObject _target;
    NavMeshAgent _agent;
    EnemyLookAt enemyLookAt;
    private EnemyIAController _enemyIAController;
    bool isEnemyChaser = false;
    // private int currentWaveIndex = 0;
    private float timeToNextWave = 0f;
   private float _reductionInterval = 10f; // cada cuántos segundos
    private bool hasActiveWave;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
        
        _agent = GetComponent<NavMeshAgent>();
       
        _agent.speed = _porsuitSpeed;
        //_agent.enabled = false;

        enemyLookAt = GetComponent<EnemyLookAt>();
        _enemyIAController = GetComponent<EnemyIAController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        isEnemyChaser = gameObject.tag == "EnemyChaser";

        if(isEnemyChaser && _agent.isActiveAndEnabled)
        {
            _agent.SetDestination(_target.transform.position);
        }
    }


   void OnTriggerEnter(Collider other)
   {
        if (other.CompareTag("Player") && _enemyIAController.CurrentState != EnemyIAController.AIState.Sleeping)
        {
            _agent.enabled = true;
            _enemyIAController.CurrentState = EnemyIAController.AIState.Chase;            
            _agent.SetDestination(_target.transform.position);
            enemyLookAt.TargetIsAppear = true;
            AudioManager.Instance.Play("IntruderAlert");
        }
        if (other.CompareTag("Player") && gameObject.tag == "Enemy" && !hasActiveWave && _enemyIAController.CurrentState != EnemyIAController.AIState.Sleeping)
        {
            // timeToNextWave += Time.deltaTime;
            // if (timeToNextWave <= _reductionInterval)
            // {
            //     timeToNextWave = 0f;
            //     LevelManager.Instance.activateWaveEnemy = true;
            // }
            hasActiveWave = true;
            WaveManager.Instance.StartCoroutine(WaveManager.Instance.StartWaves(0));
        } 
   }

    void OnTriggerStay(Collider other)
    {
        // if(WaveManager.Instance.CurrentWaveIndex > 0 && WaveManager.Instance.CurrentWaveIndex < WaveManager.Instance.spawnerWaves.Count)
        // {
        //     hasActiveWave = false;            
        // }
    }
    void OnTriggerExit(Collider other)
   {

        if(other.CompareTag("Player")) 
        {
            //_agent.enabled = false;
            enemyLookAt.TargetIsAppear = false;
            LevelManager.Instance.activateWaveEnemy = false;
            AudioManager.Instance.Stop("IntruderAlert");
        }
   }

}
