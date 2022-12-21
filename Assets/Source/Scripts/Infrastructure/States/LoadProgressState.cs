using System;
using Source.Infrastructure.States;
using Source.Scripts;
using Source.Scripts.Infrastructure.Services.PersistentProgress;

namespace Source.Infrastructure
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IStorage _storage;
        private readonly ICoroutineRunner _coroutineRunner;
        
        public LoadProgressState(GameStateMachine gameStateMachine, IStorage storage,  ICoroutineRunner coroutineRunner)
        {
            _gameStateMachine = gameStateMachine;
            _storage = storage;
            _coroutineRunner = coroutineRunner;
        }
        
        public void Exit()
        {
        }

        public void Enter()
        {
#if UNITY_WEBGL
            LoadProgressOrInitNew(() =>
                _gameStateMachine.Enter<LoadLevelState>());
#else
            _gameStateMachine.Enter<LoadLevelState>();
#endif
        }
        
        private void LoadProgressOrInitNew(Action onRemoteDataLoaded)
        {
            _coroutineRunner.StartCoroutine(_storage.SyncRemoteSave(onRemoteDataLoaded));
        }
        
        private PlayerProgress NewProgress() => 
            new PlayerProgress("ShootingTest");
    }
}