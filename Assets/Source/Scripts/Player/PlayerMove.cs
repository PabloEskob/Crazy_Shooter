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


    private IEnumerator StartRotate()
    {
        _currentSpeedRotate = 0;
        var value = _number;
        var newVector = SetNewVector(value);

        while (_currentSpeedRotate <= _speedRotate)
        {
            yield return new WaitForSeconds(0.01f);
            _currentSpeedRotate += 0.01f;
            _splineFollower.motion.rotationOffset = Vector3.Lerp(_splineFollower.motion.rotationOffset, newVector, _currentSpeedRotate);
        }
        Stop(StartRotate());
    }

    private Vector3 SetNewVector(int value)
    {
        Vector3 directionToFace = _turningPoints.GetPoint(value).transform.position - transform.position;
        float angleBetween = Vector3.SignedAngle(directionToFace, Vector3.back, Vector3.up);
        var newVector = new Vector3(0, -angleBetween, 0);
        return newVector;
    }


    private void Stop(IEnumerator enumerator)
    {
        StopCoroutine(enumerator);
        _number++;
    }

    private void StopMove(SplineUser arg0)
    {
        StartCoroutine(StartRotate());
        Stopped?.Invoke();
        CanMove = false;
        _constructSplineComputer.SetSpeed(0);
    }
}