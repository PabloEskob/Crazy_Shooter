using System;
using System.Collections.Generic;

public class EnemyStateMachine
{
    private Dictionary<Type, IEnemyState> _states;
    private IEnemyState _currentLevelState;

    public EnemyStateMachine(Enemy enemy)
    {
        InitStates(enemy);
    }

    public void Enter<TState>() where TState : class, IEnemyState
    {
        var state = SetState<TState>();
        state.Enter();
    }

    private void InitStates(Enemy enemy)
    {
        _states = new Dictionary<Type, IEnemyState>
        {
            [typeof(WaitingEnemyState)] = new WaitingEnemyState(enemy),
            [typeof(AttackEnemyState)] = new AttackEnemyState(),
            [typeof(StateOfDeathEnemy)] = new StateOfDeathEnemy(),
            [typeof(MoveEnemyState)]= new MoveEnemyState()
        };
    }

    private TState SetState<TState>() where TState : class, IEnemyState
    {
        _currentLevelState?.Exit();
        IEnemyState state = GetState<TState>();
        _currentLevelState = state;
        return (TState)state;
    }

    private IEnemyState GetState<T>() where T :IEnemyState
    {
        var type = typeof(T);
        return _states[type];
    }
}