using System;
using InfimaGames.LowPolyShooterPack;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    [SerializeField] private float _speedRotate = 0.3f;

    private Rotate _rotate;
    private float _currentSpeedRotate;
    private float _elapsedTime;
    private Vector3 _targetVector;
    private bool _canRotate;
    private bool _canReturn;
    private CameraLook _cameraLook;

    public event Action OnTurnedAround;

    private void OnEnable() =>
        _rotate.OnTurned += OnTurned;

    private void Awake()
    {
        _rotate = GetComponentInChildren<Rotate>();
        _cameraLook = GetComponentInChildren<CameraLook>();
    }

    private void OnDisable() =>
        _rotate.OnTurned -= OnTurned;

    public void StartRotate(TurningPoint turningPoint)
    {
        _rotate.Init(turningPoint != null ? turningPoint.transform.position : new Vector3(0f,10f,0f));
    }

    public void RotateReturn() =>
        _rotate.Return();

    public void DisableCameraLock() =>
        _cameraLook.Switch(false);

    public void EnableCameraLock() =>
        _cameraLook.Switch(true);

    private void OnTurned() =>
        OnTurnedAround?.Invoke();
}