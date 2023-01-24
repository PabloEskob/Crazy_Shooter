
public class TurnState : ILevelState
{
    private readonly LevelStateMachine _levelStateMachine;
    private readonly Player _player;
    private readonly LaunchRoom _launchRoom;

    public TurnState(LevelStateMachine levelStateMachine, Player player, LaunchRoom launchRoom)
    {
        _launchRoom = launchRoom;
        _levelStateMachine = levelStateMachine;
        _player = player;
    }

    public void Enter()
    {
        var turningPoint = _launchRoom.GetRoom().GetTurningPoint();
        _player.PlayerRotate.StartRotate(turningPoint);
        _player.PlayerRotate.OnTurnedAround += Turned;
    }

    public void Exit()
    {
        _player.PlayerRotate.CameraLook.Switch(true);
        _player.PlayerRotate.OnTurnedAround -= Turned;
    }

    private void Turned()
    {
        _levelStateMachine.Enter<AttackState>();
    }
}