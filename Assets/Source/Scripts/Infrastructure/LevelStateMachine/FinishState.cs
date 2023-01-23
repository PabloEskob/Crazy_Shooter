using System.Collections;
using UnityEngine;

public class FinishState : ILevelState
{
    private readonly Player _player;
    private readonly FinishLevel _finishLevel;
    private Coroutine _coroutine;
    private bool _canRotate;

    public FinishState(LevelStateMachine levelStateMachine, Player player, FinishLevel finishLevel)
    {
        _finishLevel = finishLevel;
        _player = player;
    }

    public void Enter()
    {
        _canRotate = true;
        StartRoutine();
        _player.PlayerRotate.CameraLook.LookedAtTarget += StopRoutine;
        _player.PlayerRotate.CameraLook.Switch(true);
    }

    public void Exit()
    {
        _player.PlayerRotate.CameraLook.LookedAtTarget -= StopRoutine;
    }

    private void StartRoutine() => 
        _coroutine = Coroutines.StartRoutine(StartRotateToTarget());

    private void StopRoutine()
    {
        _canRotate = false;
        Coroutines.StopRoutine(_coroutine);
    }

    private IEnumerator StartRotateToTarget()
    {
        while (_canRotate)
        {
            yield return null;
            _player.PlayerRotate.CameraLook.UpdatePositionToLookAt(_finishLevel.TurningPoint);
        }
        
    }
}