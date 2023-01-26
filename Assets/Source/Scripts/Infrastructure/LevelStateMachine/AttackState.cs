public class AttackState : ILevelState
{
    private readonly LevelStateMachine _levelStateMachine;
    private readonly LaunchRoom _launchRoom;
    private Room _room;
    private readonly Player _player;

    public AttackState(LevelStateMachine levelStateMachine, LaunchRoom launchRoom, Player player)
    {
        _player = player;
        _launchRoom = launchRoom;
        _levelStateMachine = levelStateMachine;
    }

    public void Enter()
    {
        _player.PlayerRotate.EnableCameraLock();
        _room = _launchRoom.GetRoom();
        _room.OnRoomCleared += RoomClear;
    }

    public void Exit()
    {
        _player.PlayerRotate.CameraLook.StopRoutine();
       
        _room.OnRoomCleared -= RoomClear;
    }

    private void RoomClear()
    {
        _levelStateMachine.Enter<MoveState>();
    }
}