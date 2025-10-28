using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAINavigation : MonoBehaviour
{
    
    [SerializeField] float _porsuitSpeed = 3.0f;

    GameObject _target;
    NavMeshAgent _agent;
    EnemyLookAt enemyLookAt;
    bool isEnemyChaser = false;
    private int currentWaveIndex = 0;
    private float timeToNextWave = 0f;
   private float _reductionInterval = 10f; // cada cuántos segundos


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
        
        _agent = GetComponent<NavMeshAgent>();
       
        _agent.speed = _porsuitSpeed;
        //_agent.enabled = false;

        enemyLookAt = GetComponent<EnemyLookAt>();
        isEnemyChaser = gameObject.tag == "EnemyChaser";
    }

    // Update is called once per frame
    void Update()
    {
        if(_target == null) return;

        if(isEnemyChaser)
            if (_agent.enabled)
                _agent.SetDestination(_target.transform.position);
        
    }


   void OnTriggerEnter(Collider other)
   {
        if (other.CompareTag("Player"))
        {
            _agent.enabled = true;
            enemyLookAt.TargetIsAppear = true;
        }
   }

    void OnTriggerStay(Collider other)
    {
         if (other.CompareTag("Player"))
        {
            Debug.Log("stay");
                        Debug.Log(timeToNextWave);
            timeToNextWave += Time.deltaTime;
            if (timeToNextWave <= _reductionInterval)
            {
                timeToNextWave = 0f;
                LevelManager.Instance.activateWaveEnemy = true;
            }
        } 
    }
    void OnTriggerExit(Collider other)
   {
        if(other.CompareTag("Player")) 
        {
            //_agent.enabled = false;
            enemyLookAt.TargetIsAppear = false;
            LevelManager.Instance.activateWaveEnemy = false;
        }
   }

}
