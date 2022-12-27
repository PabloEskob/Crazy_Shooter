using System;
using Source.Infrastructure.States;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
using Source.Scripts.Ui;
using UnityEngine;

namespace Source.Infrastructure
{
    public class LoadMapSceneState : IState
    {
        private const string InterfaceDataPath = "UI/Interface";
        private const string MapSceneName = "MapScene";
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IStorage _storage;
        private readonly LoadingScreen _loadingScreen;

        public LoadMapSceneState(GameStateMachine stateMachine, SceneLoader sceneLoader, IStorage storage, LoadingScreen loadingScreen)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _storage = storage;
            _loadingScreen = loadingScreen;
        }
        
        
        public void Exit() => 
            _loadingScreen.Hide();

        public void Enter() => 
            _sceneLoader.Load(MapSceneName, OnLoaded);

        private void OnLoaded()
        {
            _stateMachine.Enter<GameLoopState>();
            GameObject mainMap = AllServices.Container.Single<IAssetProvider>().Instantiate(InterfaceDataPath);
            mainMap.GetComponent<MainMap>().Cunstruct(_sceneLoader, _stateMachine, _storage);
        }
    }
}