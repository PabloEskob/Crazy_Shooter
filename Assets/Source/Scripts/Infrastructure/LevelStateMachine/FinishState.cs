using UnityEngine;

public class FinishState : ILevelState
{
    private readonly Player _player;
    private readonly FinishLevel _finishLevel;
    private Coroutine _coroutine;

    public FinishState(LevelStateMachine levelStateMachine, Player player, FinishLevel finishLevel)
    {
        _finishLevel = finishLevel;
        _player = player;
    }

    public void Enter()
    {
        _player.PlayerRotate.CameraLook.StartRotateToFinish(_finishLevel.TurningPoint);
    }

    public void Exit()
    {
       
    }
}