using UnityEngine;

public class EnemyLookAt : MonoBehaviour 
{
    [SerializeField] Transform _target;
    
    [SerializeField] Transform _bulletSpawner;
    
    [SerializeField] GameObject _bullet;

    [SerializeField] float _timer;

    bool _targetIsAppear;
    bool _enemyIsSleep;
    public bool TargetIsAppear{set { _targetIsAppear = value;} }
    public bool EnemySleeped{set { _enemyIsSleep = value;} }
    float _count;
    
    private void Update() 
    {
        if(!_targetIsAppear) return;
        if (_enemyIsSleep) return;

        _count += Time.deltaTime;

        if (_count >= _timer)
        {
            _count = 0;
            AudioManager.Instance.Play("LaserShoot");
            Shoot();
        }
       
       // transform.LookAt(_target , Vector3.up);

    }
    void Shoot()
    {
        
        GameObject bullet = Instantiate(_bullet, _bulletSpawner) as GameObject;
     
    }


}


