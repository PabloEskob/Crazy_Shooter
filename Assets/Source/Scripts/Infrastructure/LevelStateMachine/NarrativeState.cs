using UnityEngine;

public class NarrativeState : ILevelState
{
    private readonly LevelStateMachine _levelStateMachine;
    private readonly GameStatusScreen _gameStatusScreen;
    
    public NarrativeState(LevelStateMachine levelStateMachine, GameStatusScreen gameStatusScreen)
    {
        _levelStateMachine = levelStateMachine;
        _gameStatusScreen = gameStatusScreen;
    }

    public void Enter()
    {
        _gameStatusScreen.PlayNarrative();
    }

    public void Exit()
    {
        _gameStatusScreen.EndNarrative();
    }
}