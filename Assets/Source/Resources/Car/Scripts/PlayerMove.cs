﻿using System;
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

    private void OnDisable() =>
        RemoveListenerSplineTrigger();

    public void Construct(SplineComputer splineComputer)
    {
        _constructSplineComputer = GetComponent<ConstructSplineComputer>();
        _constructSplineComputer.Construct(splineComputer);
        CreateSplineTrigger();
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