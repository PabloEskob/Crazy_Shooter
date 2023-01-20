using System;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _speedReturn;
    
    private Vector3 _turningPoint;
    private bool _canRotate;
    private float _elapsedTime;
    private bool _canReturn;
    private TurningPoint _playerPoint;

    public event Action OnTurned;
    
    private void Update()
    {
        if (_canRotate)
        {
            _elapsedTime += Time.deltaTime;
            float percentageCompleted = _elapsedTime / _speed;
            LookAtXZ(transform, _turningPoint, percentageCompleted);
        }

        if (_canRotate == false && _canReturn)
        {
            _elapsedTime += Time.deltaTime;
            float percentageCompleted = _elapsedTime / _speedReturn;
            LookAtXZ(transform, _playerPoint.transform.position, percentageCompleted);
        }
    }

    public void Init(Vector3 turningPoint)
    {
        _turningPoint = turningPoint;
        _elapsedTime = 0;
        _canRotate = true;
    }

    private void LookAtXZ(Transform transform, Vector3 point, float speed)
    {
        var direction = (point - transform.position).normalized;
        direction.y = 0f;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), speed);

        if (transform.rotation == Quaternion.LookRotation(direction))
        {
            _canReturn = false;
            _canRotate = false;
            OnTurned?.Invoke();
        }
    }

    public void Return(TurningPoint turningPoint)
    {
        _playerPoint = turningPoint;
        _canReturn = true;
        _elapsedTime = 0;
    }
}