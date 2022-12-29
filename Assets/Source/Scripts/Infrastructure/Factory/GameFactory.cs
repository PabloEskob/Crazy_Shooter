using System.Collections.Generic;
using Source.Infrastructure;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace Source.Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private const string LaunchRoomTag = "LaunchRoom";
        private const string ActorUiTag = "ActorUi";
        private readonly IStaticDataService _staticDataEnemy;
        private readonly IAssetProvider _assetProvider;

        public Player Player { get; private set; }
        public List<ISavedProgressReader> ProgressReaders { get; }
        public List<ISavedProgress> ProgressWriters { get; }

        public GameFactory(IAssetProvider assetProvider, IStaticDataService staticDataEnemy)
        {
            _staticDataEnemy = staticDataEnemy;
            _assetProvider = assetProvider;
        }

        // public Player CreatePlayer(Transform position)
        // {
        //     Player = _assetProvider.Instantiate(AssetPath.PlayerPath,position).GetComponent<Player>();
        //     return Player;
        // }

        public Player CreatePlayer(GameObject initialPoint) =>
            InstantiateRegistered(AssetPath.PlayerPath, initialPoint.transform.position);

        public void CreateHUD()
        {
            
        }

        public void CreateStartScene()
        {
            StartScene startScene = _assetProvider.Instantiate(AssetPath.StartScenePath).GetComponent<StartScene>();
            LaunchRoom launchRoom = GameObject.FindGameObjectWithTag(LaunchRoomTag).GetComponent<LaunchRoom>();
            startScene.Construct(this, launchRoom);
        }

        public Enemy CreateEnemy(MonsterTypeId monsterTypeId, Transform parent)
        {
            var enemyStaticData = _staticDataEnemy.ForEnemy(monsterTypeId);
            var enemy = Object.Instantiate(enemyStaticData.Prefab, parent.position, Quaternion.identity);
            CreateStatsEnemy(enemy, enemyStaticData);
            CreateStatsNavMesh(enemy, enemyStaticData);
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

        private static void CreateStatsEnemy(Enemy enemy, EnemyStaticData enemyStaticData)
        {
            enemy.EnemyHealth.Max = enemyStaticData.Hp;
            enemy.EnemyAttack.Damage = enemyStaticData.Damage;
            enemy.EnemyAttack.AttackCooldown = enemyStaticData.AttackCooldown;
        }

        private void CreateStatsNavMesh(Enemy enemy, EnemyStaticData enemyStaticData)
        {
            var stats = enemy.GetComponent<NavMeshAgent>();
            stats.speed = enemyStaticData.Speed;
            stats.stoppingDistance = enemyStaticData.EffectiveDistance;
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