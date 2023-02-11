using Assets.Source.Scripts.Analytics;
using System;
using System.Collections.Generic;

public class LevelStateMachine
{
    private Dictionary<Type, ILevelState> _states;
    private ILevelState _currentLevelState;

    public LevelStateMachine(Player player, FinishLevel finishLevel, IAnalyticManager analyticManager,
        LevelAdjustmentTool levelAdjustmentTool, GameStatusScreen gameStatusScreen)
    {
        InitStates(player, finishLevel, analyticManager, levelAdjustmentTool,gameStatusScreen);
        Enter<StartLevelState>();
    }

    public void Enter<TState>() where TState : class, ILevelState
    {
        var state = SetState<TState>();
        state.Enter();
    }

    private void InitStates(Player player, FinishLevel finishLevel, IAnalyticManager analyticManager,
        LevelAdjustmentTool levelAdjustmentTool,GameStatusScreen gameStatusScreen)
    {
        _states = new Dictionary<Type, ILevelState>
        {
            [typeof(StartLevelState)] = new StartLevelState(this, analyticManager),
            [typeof(SpawnEnemyState)] = new SpawnEnemyState(this, levelAdjustmentTool),
            [typeof(AttackState)] = new AttackState(this, player, levelAdjustmentTool,gameStatusScreen),
            [typeof(MoveState)] = new MoveState(this, player),
            [typeof(TurnStateToTarget)] = new TurnStateToTarget(this, player, levelAdjustmentTool),
            [typeof(FinishState)] = new FinishState( player, finishLevel,gameStatusScreen),
            [typeof(DeathState)] = new DeathState(player,gameStatusScreen),
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