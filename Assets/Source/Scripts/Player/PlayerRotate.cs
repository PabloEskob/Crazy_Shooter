using System;
using InfimaGames.LowPolyShooterPack;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    private Rotate _rotate;
    private float _currentSpeedRotate;
    private float _elapsedTime;
    private Vector3 _targetVector;
    private bool _canRotate;
    private bool _canReturn;
    private CameraLook _cameraLook;
    private TurningPoint _playerPoint;

    public event Action OnTurnedAround;

    private void OnEnable() =>
        _rotate.OnTurned += OnTurned;

    private void Awake()
    {
        _playerPoint = GetComponentInChildren<TurningPoint>();
        _rotate = GetComponentInChildren<Rotate>();
        _cameraLook = GetComponentInChildren<CameraLook>();
    }

    private void OnDisable() =>
        _rotate.OnTurned -= OnTurned;

    public void StartRotate(TurningPoint turningPoint) => 
        _rotate.Init(turningPoint != null ? turningPoint.transform.position : new Vector3(0f,10f,0f));

    public void RotateReturn() => 
        _rotate.Return(_playerPoint);

    public void DisableCameraLock() =>
        _cameraLook.Switch(false);

    public void EnableCameraLock() =>
        _cameraLook.Switch(true);

    public void LookAt(TurningPoint finishLevelTurningPoint) => 
        _rotate.LookAt(finishLevelTurningPoint);

    
    private void OnTurned() =>
        OnTurnedAround?.Invoke();
}