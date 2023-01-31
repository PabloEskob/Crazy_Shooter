public class TurnStateToTarget : ILevelState
{
    private readonly LevelStateMachine _levelStateMachine;
    private readonly Player _player;
    private readonly LevelAdjustmentTool _levelAdjustmentTool;

    public TurnStateToTarget(LevelStateMachine levelStateMachine, Player player,
        LevelAdjustmentTool levelAdjustmentTool)
    {
        _levelStateMachine = levelStateMachine;
        _player = player;
        _levelAdjustmentTool = levelAdjustmentTool;
    }

    public void Enter()
    {
        var turningPoint = _levelAdjustmentTool.GetRoom().GetTurningPoint();
        _player.PlayerRotate.StartRotate(turningPoint);
        _player.PlayerRotate.OnTurnedAround += Turned;
    }

    public void Exit()
    {
        _player.PlayerRotate.OnTurnedAround -= Turned;
    }

    private void Turned()
    {
        _levelStateMachine.Enter<AttackState>();
    }
}