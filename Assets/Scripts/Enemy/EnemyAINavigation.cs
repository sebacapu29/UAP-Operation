using System.Collections.Generic;
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
    private bool hasActiveWave;
    private List<Light> _alertLight = new List<Light>();
    float _count;
    float _timer = 1.5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
        
        _agent = GetComponent<NavMeshAgent>();
       
        _agent.speed = _porsuitSpeed;
        //_agent.enabled = false;

        enemyLookAt = GetComponent<EnemyLookAt>();
        _enemyIAController = GetComponent<EnemyIAController>();
        var objsSirenLights = GameObject.FindGameObjectsWithTag("SirenLight");
        // Debug.Log(objsSirenLights.Length + " Siren Lights found in the scene.");    
        foreach (var obj in objsSirenLights)
        {
            var sirenLight = obj.GetComponents<SirenLight>();
            // Debug.Log("Siren Light found: " + sirenLight.Length);
            if (sirenLight != null && sirenLight.Length > 0)
            { 
                foreach (var light in sirenLight)
                {
                    // Debug.Log("Adding Siren Light to alert list");
                    // Debug.Log(light);
                    _alertLight.Add(light.sirenLight);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        isEnemyChaser = gameObject.tag == "EnemyChaser";

           _count += Time.deltaTime;

        if (_count >= _timer)
        {
            _count = 0;
            if(isEnemyChaser && _agent.isActiveAndEnabled)
            {
                _agent.SetDestination(_target.transform.position);
            }
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
            ActivateAlertLight();
        }
        if (other.CompareTag("Player") && gameObject.tag == "Enemy" && !hasActiveWave && _enemyIAController.CurrentState != EnemyIAController.AIState.Sleeping)
        {
            hasActiveWave = true;
            WaveManager.Instance.StartCoroutine(WaveManager.Instance.StartWaves(0));
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
   void ActivateAlertLight()
   {    
        foreach (var light in _alertLight)
        {
            light.enabled = true;
        }
   }

}
