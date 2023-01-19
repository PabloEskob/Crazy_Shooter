public class SpawnEnemyState : ILevelState
{
    private readonly LaunchRoom _launchRoom;
    private readonly LevelStateMachine _levelStateMachine;
    private Room _room;
    private int _value;

    public SpawnEnemyState(LevelStateMachine levelStateMachine, LaunchRoom launchRoom)
    {
        _levelStateMachine = levelStateMachine;
        _launchRoom = launchRoom;
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
        if (_launchRoom.Number > _value)
        {
            _launchRoom.StartRoom(_value);
            _levelStateMachine.Enter<TurnState>();
        }
        else
        {
            _levelStateMachine.Enter<FinishState>();
        }
    }
}