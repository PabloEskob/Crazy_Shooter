using System.Collections.Generic;
using Assets.Source.Scripts.Analytics;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace Source.Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private const string FinishLevel = "FinishLevel";
        private const string LevelTag = "LevelTool";

        private readonly IStaticDataService _staticDataEnemy;
        private readonly IAnalyticManager _analyticManager;
        private readonly IAssetProvider _assetProvider;
        private GameStatusScreen _gameStatusScreen;
        private LevelStateMachine _levelStateMachine;
        private FinishLevel _finishLevel;
        private LevelAdjustmentTool _levelAdjustmentTool;

        public Player Player { get; private set; }
        public List<ISavedProgressReader> ProgressReaders { get; }
        public List<ISavedProgress> ProgressWriters { get; }

        public GameFactory(IAssetProvider assetProvider, IStaticDataService staticDataEnemy,
            IAnalyticManager analyticManager)
        {
            _staticDataEnemy = staticDataEnemy;
            _analyticManager = analyticManager;
            _assetProvider = assetProvider;
        }

        public Player CreatePlayer(GameObject initialPoint) =>
            InstantiateRegistered(AssetPath.PlayerPath, initialPoint.transform.position);

        public void CreateHUD(Player player)
        {
            _gameStatusScreen = _assetProvider.Instantiate(AssetPath.PathGameStatusScreen)
                .GetComponent<GameStatusScreen>();
            _gameStatusScreen.Player = player;
        }

        public void CreateStartScene()
        {
            
            _finishLevel = GameObject.FindGameObjectWithTag(FinishLevel).GetComponent<FinishLevel>();
            _levelAdjustmentTool = GameObject.FindGameObjectWithTag(LevelTag).GetComponent<LevelAdjustmentTool>();
            StartScene startScene = _assetProvider.Instantiate(AssetPath.StartScenePath).GetComponent<StartScene>();
            startScene.Construct(this, _gameStatusScreen, _finishLevel,_levelAdjustmentTool);
        }

        public Enemy CreateEnemy(MonsterTypeId monsterTypeId, Vector3 parent, bool move, EnemySpawner enemySpawner)
        {
            var enemyStaticData = _staticDataEnemy.ForEnemy(monsterTypeId);
            var enemy = Object.Instantiate(enemyStaticData.Prefab, parent, Quaternion.identity);
            CreateStatsEnemy(enemy, move, enemySpawner);
            CreateStatsNavMesh(enemy, enemySpawner);
            return enemy;
        }

        public void CreateLevelStateMachine(Player player, IAnalyticManager analyticManager) =>
            _levelStateMachine = new LevelStateMachine(player,  _finishLevel, analyticManager,_levelAdjustmentTool);

        private Player InstantiateRegistered(string prefabPath, Vector3 position)
        {
            Player = _assetProvider.Instantiate(prefabPath, position).GetComponent<Player>();
            Player.PlayerHealth.LoadProgress(NewProgress());
            RegisterProgressWatchers(Player.gameObject);
            return Player;
        }

        private PlayerProgress NewProgress()
        {
            var progress = new PlayerProgress();
            progress.CarState.MaxHp = 50;
            progress.CarState.ResetHp();
            return progress;
        }

        private static void CreateStatsEnemy(Enemy enemy, bool move, EnemySpawner enemySpawner)
        {
            enemy.EnemyHealth.Max = enemySpawner.Hp;
            enemy.EnemyAttack.Damage = enemySpawner.Damage;
            enemy.EnemyAttack.AttackCooldown = enemySpawner.AttackCooldown;
            enemy.EnemyMove.SetCanMove(move);
        }

        private void CreateStatsNavMesh(Enemy enemy, EnemySpawner enemySpawner)
        {
            var stats = enemy.GetComponent<NavMeshAgent>();
            enemy.EnemyMove.Speed = enemySpawner.Speed;
            stats.stoppingDistance = enemySpawner.EffectiveDistance;
        }

        private void RegisterProgressWatchers(GameObject instantiatePlayer)
        {
            foreach (ISavedProgressReader progressReader in instantiatePlayer
                         .GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }
    }
}