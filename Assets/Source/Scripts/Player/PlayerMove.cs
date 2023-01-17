using System;
using Dreamteck.Splines;
using UnityEngine;

[RequireComponent(typeof(ConstructSplineComputer))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed;

    private ConstructSplineComputer _constructSplineComputer;
    private TriggerGroup _triggerGroup;
    private bool _move;
    private Player _player;

    public bool CanMove { get; set; }

    public event Action Stopped;
    public event Action Disabled;

    private void Awake()
    {
        _player = GetComponent<Player>();
        SplineComputer spline = FindObjectOfType<SplineComputer>();
        _constructSplineComputer = GetComponent<ConstructSplineComputer>();
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
        _constructSplineComputer.SetSpeed(_speed);
        _player.PlayerRotate.RotateReturn();
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
        _player.PlayerRotate.StartRotate();
        Stopped?.Invoke();
        CanMove = false;
        _constructSplineComputer.SetSpeed(0);
    }
}