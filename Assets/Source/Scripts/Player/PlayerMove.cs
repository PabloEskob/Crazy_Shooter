using System;
using Dreamteck.Splines;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ConstructSplineComputer))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed;

    private ConstructSplineComputer _constructSplineComputer;
    private TriggerGroup _triggerGroup;

    public bool CanMove { get; set; }

    public event Action Stopped;
    public event Action Disabled;

    private void Awake()
    {
        _constructSplineComputer = GetComponent<ConstructSplineComputer>();
        SplineComputer spline = FindObjectOfType<SplineComputer>();
        _constructSplineComputer.Construct(spline);
        CreateSplineTrigger();
    }

    private void OnDisable()
    {
        RemoveListenerSplineTrigger();
        Disabled?.Invoke();
    }
    
    public void PlayMove()
    {
        if (CanMove)
            _constructSplineComputer.SetSpeed(_speed);
    }

    public void PlayMove(InputAction.CallbackContext context)
    {
        if (CanMove)
            _constructSplineComputer.SetSpeed(_speed);
    }


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

    private void StopMove(SplineUser arg0)
    {
        Stopped?.Invoke();
        CanMove = false;
        _constructSplineComputer.SetSpeed(0);
    }
}