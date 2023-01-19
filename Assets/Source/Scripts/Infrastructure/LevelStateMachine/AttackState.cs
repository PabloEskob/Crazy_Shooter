public class AttackState : ILevelState
{
    private LevelStateMachine _levelStateMachine;
    private LaunchRoom _launchRoom;
    private Room _room;

    public AttackState(LevelStateMachine levelStateMachine, LaunchRoom launchRoom)
    {
        _launchRoom = launchRoom;
        _levelStateMachine = levelStateMachine;
    }

    public void Enter()
    {
        _room = _launchRoom.GetRoom();
        _room.OnRoomCleared += StartMoving;
    }

    public void Exit()
    {
        _room.OnRoomCleared -= StartMoving;
    }

    private void StartMoving()
    {
        _levelStateMachine.Enter<MoveState>();
    }
}