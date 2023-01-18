using Source.Infrastructure.States;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Source.Infrastructure
{
    public class LoadLevelState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IStorage _storage;
        private readonly LoadingScreen _loadingScreen;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;
        private readonly IStaticDataService _staticData;

        private string LevelName = "Level 1";

        private const string PlayerInitialPointTag = "PlayerInitialPointTag";


        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, IStorage storage,
            LoadingScreen loadingScreen, IGameFactory gameFactory, IStaticDataService staticData)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _storage = storage;
            _loadingScreen = loadingScreen;
            _gameFactory = gameFactory;
            _staticData = staticData;
        }

        public void Enter(int level) => 
            _sceneLoader.Load(GetNextLevelNameByNumber(level), OnLoaded);

        public void Enter() => 
            _sceneLoader.Load(GetNextLevelName(), OnLoaded);

        public void Exit() => _loadingScreen.Hide();

        private void OnLoaded()
        {
            InitGameWorld();
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InitGameWorld()
        {
            Player player = _gameFactory.CreatePlayer(GameObject.FindWithTag(PlayerInitialPointTag));
            _gameFactory.CreateHUD(player);
            _gameFactory.CreateStartScene();
        }

        private string GetNextLevelName() =>
            _staticData.ForLevel(_storage.GetLevel()).SceneName;

        public string GetNextLevelNameByNumber(int levelNumber) =>
            _staticData.ForLevel(levelNumber).SceneName;
    }
}