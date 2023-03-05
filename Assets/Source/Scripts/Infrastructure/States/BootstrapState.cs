using Assets.Source.Scripts.Analytics;
using Source.Infrastructure.States;
using Source.Scripts.Infrastructure.Factory;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
using Source.Scripts.SaveSystem;
using System.Collections.Generic;

namespace Source.Infrastructure
{
    public class BootstrapState : IState
    {
        private const string InitSceneName = "InitScene";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;
        
        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        private void RegisterServices()
        {
            RegisterStaticData();
            _services.RegisterSingle<IGameStateMachine>(_stateMachine);
            _services.RegisterSingle<IAnalyticManager>(CreateAnalyticManager());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>(), _services.Single<IStaticDataService>(), _services.Single<IAnalyticManager>()));
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>(), _services.Single<IGameFactory>()));
            _services.RegisterSingle<IStorage>(new Storage(DataNames.GameName));
        }

        private void RegisterStaticData()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.LoadEnemy();
            staticData.LoadGameConfig();
            _services.RegisterSingle(staticData);
        }

        public void Enter() =>
            _sceneLoader.Load(InitSceneName, onLoaded: EnterLoadLevel);

        public void Exit()
        {
        }

        private void EnterLoadLevel() =>
            _stateMachine.Enter<LoadProgressState>();

        public void Enter(int level)
        {
            _sceneLoader.Load(InitSceneName, onLoaded: EnterLoadLevel);
        }

        private IAnalyticManager CreateAnalyticManager()
        {
            IAnalyticManager analytic = new AnalyticManager(new List<IAnalytic>
            {
                new GameAnalyticsAnalytic()
            });
//#if YANDEX_METRICA && !UNITY_EDITOR
//            analytic.AddAnalytic(new YandexMetricaAnalytic());
//#endif
            return analytic;
        }

        public class PersistentProgressService : IPersistentProgressService
        {
            public PlayerProgress Progress { get; set; }
        }
    }
}