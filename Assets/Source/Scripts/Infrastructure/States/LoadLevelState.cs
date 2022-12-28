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

        private const string PlayerInitialPointTag = "PlayerInitialPointTag";
        private const string EnemySpawnerTag = "EnemySpawner";

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

        public void Enter()
        {
            _loadingScreen.Show();
            //_gameFactory.Cleanup();
            _sceneLoader.Load("NewScene", OnLoaded);
        }

        public void Exit() => _loadingScreen.Hide();

        private void OnLoaded()
        {
            InitGameWorld();
            _gameStateMachine.Enter<GameLoopState>();
        }


        private void InformProgressReaders()
        {
            // foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            //     progressReader.LoadProgress(_progressService.Progress);
        }

        private void InitGameWorld()
        {
            Player player = _gameFactory.CreatePlayer(GameObject.FindWithTag(PlayerInitialPointTag));
            // _gameFactory.CreateHUD();
            InitSpawners();
            _gameFactory.CreateStartScene();
            _gameFactory.CreateHUD();
            // _launchRoom.Fill(_gameFactory);
        }

        private void InitSpawners()
        {
            foreach (GameObject spawnerGameObject in GameObject.FindGameObjectsWithTag(EnemySpawnerTag))
            {
                var spawner = spawnerGameObject.GetComponent<EnemySpawner>();
                spawner.Construct(_gameFactory);
                spawner.CreateQuantityEnemy();
            }
        }
    }
}