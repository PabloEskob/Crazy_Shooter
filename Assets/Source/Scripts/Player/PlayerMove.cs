using System;
using System.Collections;
using Dreamteck.Splines;
using UnityEngine;

[RequireComponent(typeof(ConstructSplineComputer))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _speedRotate;

    private ConstructSplineComputer _constructSplineComputer;
    private SplineFollower _splineFollower;
    private TriggerGroup _triggerGroup;
    private TurningPoints _turningPoints;
    private bool _move;
    private int _number;
    private float _currentSpeedRotate;
    private float _elapsedTime;

    public bool CanMove { get; set; }

    public event Action Stopped;
    public event Action Disabled;

    private void Awake()
    {
        SplineComputer spline = FindObjectOfType<SplineComputer>();
        _constructSplineComputer = GetComponent<ConstructSplineComputer>();
        _splineFollower = GetComponent<SplineFollower>();
        _constructSplineComputer.Construct(spline);
        CreateSplineTrigger();
    }

    private void OnDisable()
    {
        RemoveListenerSplineTrigger();
        Disabled?.Invoke();
    }

    private void Start() =>
        _turningPoints = GameObject.FindGameObjectWithTag("TurningPoints").GetComponent<TurningPoints>();

    public void PlayMove() =>
        _constructSplineComputer.SetSpeed(_speed);

    private void CreateSplineTrigger()
    {
        foreach (var triggerGroup in _constructSplineComputer.GetTriggerGroup())
        foreach (var splineTrigger in triggerGroup.triggers)
            splineTrigger.AddListener(StopMove);
    }

    private void RemoveListenerSplineTrigger()
    {
        foreach (var triggerGroup in _constructSplineComputer.GetTriggerGroup())
        foreach (var splineTrigger in triggerGroup.triggers)
            splineTrigger.RemoveListener(StopMove);
    }


    private void StartRotate()
    {
        var value = _number;
        var newVector = SetNewVector(value);

        StartCoroutine(RotateSplineFollower(newVector));

        _number++;
    }


    private IEnumerator RotateSplineFollower(Vector3 vector)
    {
        _currentSpeedRotate = 0;

        while (_currentSpeedRotate <= _speedRotate)
        {
            yield return new WaitForSeconds(0.01f);
            _currentSpeedRotate += 0.01f;
            _splineFollower.motion.rotationOffset =
                Vector3.Lerp(_splineFollower.motion.rotationOffset, vector, _currentSpeedRotate);
            Debug.Log($"{_splineFollower.motion.rotationOffset}-{vector}");
        }

        Stop(RotateSplineFollower(vector));
    }

    private Vector3 SetNewVector(int value)
    {
        var turningPoint = _turningPoints.GetPoint(value);

        if (turningPoint != null)
        {
            Vector3 directionToFace = turningPoint.transform.position - transform.position;
            float angleBetween = Vector3.SignedAngle(directionToFace, Vector3.back, Vector3.up);
            var newVector = new Vector3(0, -angleBetween, 0);
            return newVector;
        }

        return _splineFollower.motion.rotationOffset;
    }


    private void Stop(IEnumerator enumerator) =>
        StopCoroutine(enumerator);

    private void StopMove(SplineUser arg0)
    {
        StartRotate();
        Stopped?.Invoke();
        CanMove = false;
        _constructSplineComputer.SetSpeed(0);
    }
}