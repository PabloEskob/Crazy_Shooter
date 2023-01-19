using System;
using Dreamteck.Splines;
using InfimaGames.LowPolyShooterPack;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    [SerializeField] private float _speedRotate = 0.3f;
    
    private float _currentSpeedRotate;
    private SplineFollower _splineFollower;
    private float _elapsedTime;
    private Vector3 _targetVector;
    private bool _canRotate;
    private bool _canReturn;
    private CameraLook _cameraLook;

    public event Action OnTurnedAround;

    private void Awake()
    {
        _splineFollower = GetComponent<SplineFollower>();
        _cameraLook = GetComponentInChildren<CameraLook>();
    }
    
    private void Update()
    {
        if (_canRotate)
            RotateTo(_targetVector);

        if (_canReturn)
            RotateTo(Vector3.zero);
    }

    public void StartRotate(TurningPoint turningPoint)
    {
        if (turningPoint == null)
            RotateReturn();
        else
        {
            _targetVector = SetNewVector(turningPoint);
            _canRotate = true;
        }
        
    }

    public void RotateReturn() => 
        _canReturn = true;

    public void DisableCameraLock() => 
        _cameraLook.Switch(false);

    public void EnableCameraLock() => 
        _cameraLook.Switch(true);

    private void RotateTo(Vector3 target)
    {
        if (_splineFollower.motion.rotationOffset != target)
            Rotate(target);
        else
        {
            OnTurnedAround?.Invoke();
            _elapsedTime = 0;
            _canRotate = false;
            _canReturn = false;
        }
    }
    
    private void Rotate(Vector3 target)
    {
        _elapsedTime += Time.deltaTime;
        float percentageCompleted = _elapsedTime / _speedRotate;
        _splineFollower.motion.rotationOffset =
            Vector3.MoveTowards(_splineFollower.motion.rotationOffset, target, percentageCompleted);
    }

    private Vector3 SetNewVector(TurningPoint turningPoint)
    {
        if (turningPoint != null)
        {
            Vector3 directionToFace = turningPoint.transform.position - transform.position;
            float angleBetween = Vector3.SignedAngle(directionToFace, Vector3.back, Vector3.up);
            var newVector = new Vector3(0, -angleBetween, 0);
            return newVector;
        }

        return _splineFollower.motion.rotationOffset;
    }
}