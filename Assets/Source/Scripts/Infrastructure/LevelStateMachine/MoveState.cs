﻿public class MoveState : ILevelState
{
    private readonly Player _player;
    private readonly LevelStateMachine _levelStateMachine;

    public MoveState(LevelStateMachine levelStateMachine, Player player)
    {
        _levelStateMachine = levelStateMachine;
        _player = player;
    }

    public void Enter()
    {
        _player.PlayerRotate.RotateReturn();
        _player.PlayerRotate.DisableCameraLock();
        _player.Character.NoFire();
        _player.PlayerMove.PlayMove();
        _player.PlayerMove.OnStopped += Stopped;
    }

    public void Exit()
    {
        _player.PlayerRotate.CameraLook.StopRoutine();
        _player.PlayerMove.OnStopped -= Stopped;
        _player.PlayerMove.StopMove();
        _player.Character.YesFire();
    }

    private void Stopped()
    {
        _levelStateMachine.Enter<SpawnEnemyState>();
    }
}