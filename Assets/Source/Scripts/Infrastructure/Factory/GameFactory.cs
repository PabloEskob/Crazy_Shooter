using System.Collections.Generic;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace Source.Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private const string LaunchRoomTag = "LaunchRoom";
        private const string FinishLevel = "FinishLevel";

        private readonly IStaticDataService _staticDataEnemy;
        private readonly IAssetProvider _assetProvider;

        private GameStatusScreen _gameStatusScreen;

        public Player Player { get; private set; }
        public List<ISavedProgressReader> ProgressReaders { get; }
        public List<ISavedProgress> ProgressWriters { get; }

        public GameFactory(IAssetProvider assetProvider, IStaticDataService staticDataEnemy)
        {
            _staticDataEnemy = staticDataEnemy;
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
            StartScene startScene = _assetProvider.Instantiate(AssetPath.StartScenePath).GetComponent<StartScene>();
            LaunchRoom launchRoom = GameObject.FindGameObjectWithTag(LaunchRoomTag).GetComponent<LaunchRoom>();
            FinishLevel finishLevel = GameObject.FindGameObjectWithTag(FinishLevel).GetComponent<FinishLevel>();
            startScene.Construct(this, launchRoom, _gameStatusScreen,finishLevel);
        }

        public Enemy CreateEnemy(MonsterTypeId monsterTypeId, Transform parent, bool move, EnemySpawner enemySpawner)
        {
            var enemyStaticData = _staticDataEnemy.ForEnemy(monsterTypeId);
            var enemy = Object.Instantiate(enemyStaticData.Prefab, parent.position, Quaternion.identity);
            CreateStatsEnemy(enemy, move, enemySpawner);
            CreateStatsNavMesh(enemy, enemySpawner);
            return enemy;
        }

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
            enemy.EnemyMove.CanMove(move);
        }

        private void CreateStatsNavMesh(Enemy enemy, EnemySpawner enemySpawner)
        {
            var stats = enemy.GetComponent<NavMeshAgent>();
            stats.speed = enemySpawner.Speed;
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