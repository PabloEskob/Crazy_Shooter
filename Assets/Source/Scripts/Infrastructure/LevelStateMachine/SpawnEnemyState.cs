public class SpawnEnemyState : ILevelState
{
    private readonly LevelStateMachine _levelStateMachine;
    private int _value;
    private readonly LevelAdjustmentTool _levelAdjustmentTool;
    private readonly StartAlert _startAlert;

    public SpawnEnemyState(LevelStateMachine levelStateMachine, LevelAdjustmentTool levelAdjustmentTool,
        StartAlert startAlert)
    {
        _levelStateMachine = levelStateMachine;
        _levelAdjustmentTool = levelAdjustmentTool;
        _startAlert = startAlert;
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
            if (_startAlert.ChooseDialoguePlaceCurrent != StartAlert.ChooseDialoguePlace.Start)
            {
                _startAlert.StartEndDialogue();
                _levelStateMachine.Enter<NarrativeState>();
            }
            else
                _levelStateMachine.Enter<FinishState>();

        }
    }
}