﻿using Assets.Source.Scripts.Analytics;
using System;
using System.Collections.Generic;

public class LevelStateMachine
{
    private Dictionary<Type, ILevelState> _states;
    private ILevelState _currentLevelState;

    public LevelStateMachine(Player player, LaunchRoom launchRoom, FinishLevel finishLevel, IAnalyticManager analyticManager)
    {
        InitStates(player, launchRoom, finishLevel, analyticManager);
        Enter<StartLevelState>();
    }

    public void Enter<TState>() where TState : class, ILevelState
    {
        var state = SetState<TState>();
        state.Enter();
    }

    private void InitStates(Player player, LaunchRoom launchRoom, FinishLevel finishLevel, IAnalyticManager analyticManager)
    {
        _states = new Dictionary<Type, ILevelState>
        {
            [typeof(StartLevelState)] = new StartLevelState(this, analyticManager),
            [typeof(SpawnEnemyState)] = new SpawnEnemyState(this, launchRoom),
            [typeof(AttackState)] = new AttackState(this, launchRoom, player),
            [typeof(MoveState)] = new MoveState(this, player),
            [typeof(TurnStateToTarget)] = new TurnStateToTarget(this, player, launchRoom),
            [typeof(FinishState)] = new FinishState(this, player, finishLevel),
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