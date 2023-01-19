using UnityEngine;

public class FinishState : ILevelState
{
    private readonly Player _player;

    public FinishState(LevelStateMachine levelStateMachine, Player player)
    {
        _player = player;
    }

    public void Enter()
    {
        _player.PlayerRotate.EnableCameraLock();
    }

    public void Exit()
    {
        Debug.Log("FinishExit");
    }
}