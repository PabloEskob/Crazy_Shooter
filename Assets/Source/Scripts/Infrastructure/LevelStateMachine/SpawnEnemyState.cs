public class SpawnEnemyState : ILevelState
{
    private readonly LevelStateMachine _levelStateMachine;
    private int _value;
    private readonly LevelAdjustmentTool _levelAdjustmentTool;

    public SpawnEnemyState(LevelStateMachine levelStateMachine,LevelAdjustmentTool levelAdjustmentTool)
    {
        _levelStateMachine = levelStateMachine;
        _levelAdjustmentTool = levelAdjustmentTool;
    }

    public void Enter()
    {
        SetRoom();
    }

    public void Exit()
    {
        _value++;
    }

    private void SetRoom()
    {
        if (_levelAdjustmentTool.CountZones > _value)
        {
            _levelAdjustmentTool.StartRoom(_value);
            _levelStateMachine.Enter<TurnStateToTarget>();
        }
        else
        {
            _levelStateMachine.Enter<FinishState>();
        }
    }
}