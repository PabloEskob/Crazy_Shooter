using UnityEngine;

public class AttackState : ILevelState
{
    private readonly LevelStateMachine _levelStateMachine;

    private Zone _zone;
    private readonly Player _player;
    private readonly LevelAdjustmentTool _levelAdjustmentTool;

    public AttackState(LevelStateMachine levelStateMachine, Player player, LevelAdjustmentTool levelAdjustmentTool)
    {
        _player = player;
        _levelStateMachine = levelStateMachine;
        _levelAdjustmentTool = levelAdjustmentTool;
    }

    public void Enter()
    {
        _player.PlayerRotate.EnableCameraLock();
        _zone = _levelAdjustmentTool.GetRoom();
        _zone.OnRoomCleared += RoomClear;
        _zone.OnNextWave += ClearWave;
    }

    public void Exit()
    {
        _player.PlayerRotate.CameraLook.StopRoutine();
        _zone.OnRoomCleared -= RoomClear;
        _zone.OnNextWave -= ClearWave;
    }

    private void RoomClear()
    {
        _levelStateMachine.Enter<MoveState>();
    }

    private void ClearWave()
    {
        _levelStateMachine.Enter<TurnStateToTarget>();
    }
}