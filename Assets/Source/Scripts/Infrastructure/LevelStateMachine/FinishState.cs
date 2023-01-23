using UnityEngine;

public class FinishState : ILevelState
{
    private readonly Player _player;
    private readonly FinishLevel _finishLevel;

    public FinishState(LevelStateMachine levelStateMachine, Player player, FinishLevel finishLevel)
    {
        _finishLevel = finishLevel;
        _player = player;
    }

    public void Enter()
    {
        _player.PlayerRotate.LookAt(_finishLevel.TurningPoint);
        _player.PlayerRotate.EnableCameraLock();
    }

    public void Exit()
    {
        Debug.Log("FinishExit");
    }
}