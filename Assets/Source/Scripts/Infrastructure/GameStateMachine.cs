using System;
using System.Collections.Generic;
using Source.Infrastructure.States;
using Source.Scripts;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Source.Infrastructure
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingScreen loadingScreen, ICoroutineRunner coroutineRunner,
            AllServices services)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, services.Single<IStorage>(),
                    loadingScreen, services.Single<IGameFactory>(), services.Single<IStaticDataService>()),
                [typeof(LoadProgressState)] = new LoadProgressState(this, services.Single<IStorage>(), coroutineRunner),
                [typeof(FinishLevelState)] = new FinishLevelState(this,sceneLoader),
                [typeof(LoadMapSceneState)] =
                    new LoadMapSceneState(this, sceneLoader, services.Single<IStorage>(), loadingScreen),
                [typeof(GameLoopState)] = new GameLoopState(this)
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
    }
}