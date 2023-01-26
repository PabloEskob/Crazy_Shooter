using System;
using Dreamteck.Splines;
using UnityEngine;

[RequireComponent(typeof(ConstructSplineComputer))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed;

    private ConstructSplineComputer _constructSplineComputer;
    private TriggerGroup _triggerGroup;
    private Player _player;
    
    public event Action OnStopped;
    public event Action OnMove;
    
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
    }

    public void PlayMove()
    {
        _player.PlayerAnimator.PlayWalking();
        _constructSplineComputer.SetSpeed(_speed);
        OnMove?.Invoke();
    }

    public void StopMove()
    {
        _player.PlayerAnimator.StopWalking();
        _constructSplineComputer.SetSpeed(0);
    }

    private void CreateSplineTrigger()
    {
        foreach (var triggerGroup in _constructSplineComputer.GetTriggerGroup())
        foreach (var splineTrigger in triggerGroup.triggers)
            splineTrigger.AddListener(Stop);
    }

    private void RemoveListenerSplineTrigger()
    {
        foreach (var triggerGroup in _constructSplineComputer.GetTriggerGroup())
        foreach (var splineTrigger in triggerGroup.triggers)
            splineTrigger.RemoveListener(Stop);
    }

    private void Stop(SplineUser arg0) => 
        OnStopped?.Invoke();
}