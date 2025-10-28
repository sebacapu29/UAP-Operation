using UnityEngine;
using UnityEngine.AI;

public class EnemyWaveIANavigation : MonoBehaviour
{
      [SerializeField] float _porsuitSpeed = 3.0f;

    GameObject _target;
    NavMeshAgent _agent;
    EnemyLookAt enemyLookAt;
    bool isEnemyChaser = false;


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
       if(other.CompareTag("Player")) 
        {
            _agent.enabled = true;
            enemyLookAt.TargetIsAppear = true;
        }
   }

      void OnTriggerExit(Collider other)
   {
        if(other.CompareTag("Player")) 
        {
            //_agent.enabled = false;
            enemyLookAt.TargetIsAppear = false;
        }
   }
}
