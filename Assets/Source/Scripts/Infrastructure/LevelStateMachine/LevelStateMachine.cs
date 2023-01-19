using System;
using System.Collections.Generic;

public class LevelStateMachine
{
    private Dictionary<Type, ILevelState> _states;
    private ILevelState _currentLevelState;

    public LevelStateMachine(Player player, LaunchRoom launchRoom)
    {
        InitStates(player, launchRoom);
        Enter<SpawnEnemyState>();
    }

    public void Enter<TState>() where TState : class, ILevelState
    {
        var state = SetState<TState>();
        state.Enter();
    }

    private void InitStates(Player player, LaunchRoom launchRoom)
    {
        _states = new Dictionary<Type, ILevelState>
        {
            [typeof(SpawnEnemyState)] = new SpawnEnemyState(this, launchRoom),
            [typeof(AttackState)] = new AttackState(this, launchRoom),
            [typeof(MoveState)] = new MoveState(this, player),
            [typeof(TurnState)] = new TurnState(this, player, launchRoom),
            [typeof(FinishState)] = new FinishState(this, player),
        };
    }

    private TState SetState<TState>() where TState : class, ILevelState
    {
        _currentLevelState?.Exit();
        ILevelState state = GetState<TState>();
        _currentLevelState = state;
        return (TState)state;
    }

    private ILevelState GetState<T>() where T : ILevelState
    {
        var type = typeof(T);
        return _states[type];
    }
}