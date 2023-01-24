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
    private TurningPoint _playerPoint;

    public CameraLook CameraLook { get; private set; }

    public event Action OnTurnedAround;

    private void OnEnable() =>
        _rotate.OnTurned += OnTurned;

    private void Awake()
    {
        _playerPoint = GetComponentInChildren<TurningPoint>();
        _rotate = GetComponentInChildren<Rotate>();
        CameraLook = GetComponentInChildren<CameraLook>();
    }

    private void OnDisable() =>
        _rotate.OnTurned -= OnTurned;

    public void StartRotate(TurningPoint turningPoint) =>
        _rotate.Init(turningPoint != null ? turningPoint.transform.position : new Vector3(0f, 10f, 0f));

    public void RotateReturn() =>
        _rotate.Return(_playerPoint);

    public void DisableCameraLock() =>
        CameraLook.Switch(false);
    
    private void OnTurned() =>
        OnTurnedAround?.Invoke();
}