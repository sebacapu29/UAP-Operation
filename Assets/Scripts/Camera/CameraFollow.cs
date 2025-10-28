using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform _target;

    [SerializeField] [Range(1, 10)] float _smoothingSpeed = 5f;

    Vector3 _offset;

    Vector3 _desiredPosition;
    Vector3 _smoothedPosition;

    void Start()
    {
        _offset = transform.position - _target.position;
    }
    void LateUpdate()
    {
        
        if (_target == null)
        {
            Debug.LogWarning("No se ha asignado un objetivo a la cámara.");
            return;
        }

        _desiredPosition = _target.position + _offset;
       
        _smoothedPosition = Vector3.Lerp(transform.position, _desiredPosition, _smoothingSpeed * Time.deltaTime);

        transform.position = _smoothedPosition;
    }
}
