// using System.Collections.Generic;
// using Source.Scripts.Infrastructure.Services.PersistentProgress;
// using UnityEngine;
// using UnityEngine.AI;
// using Object = UnityEngine.Object;
//
// public class GameFactory : IGameFactory
// {
//     private readonly StaticDataService _staticDataEnemy;
//     private readonly IAssetProvider _assetProvider;
//
//     public Player Player { get; private set; }
//     public List<ISavedProgressReader> ProgressReaders { get; }
//     public List<ISavedProgress> ProgressWriters { get; }
//
//     public GameFactory(StaticDataService staticDataEnemy, IAssetProvider assetProvider)
//     {
//         _staticDataEnemy = staticDataEnemy;
//         _assetProvider = assetProvider;
//     }
//
//     public Player CreatePlayer()
//     {
//         Player = _assetProvider.Instantiate(AssetPath.PlayerPath).GetComponent<Player>();
//         return Player;
//     }
//
//     public Enemy CreateEnemy(MonsterTypeId monsterTypeId, Transform parent)
//     {
//         var enemyStaticData = _staticDataEnemy.ForEnemy(monsterTypeId);
//         var enemy = Object.Instantiate(enemyStaticData.Prefab, parent.position, Quaternion.identity);
//         CreateStatsEnemy(enemy, enemyStaticData);
//         CreateStatsNavMesh(enemy, enemyStaticData);
//         return enemy;
//     }
//
//     private static void CreateStatsEnemy(Enemy enemy, EnemyStaticData enemyStaticData)
//     {
//         enemy.EnemyHealth.Max = enemyStaticData.Hp;
//         enemy.EnemyAttack.Damage = enemyStaticData.Damage;
//         enemy.EnemyAttack.AttackCooldown = enemyStaticData.AttackCooldown;
//     }
//
//     private void CreateStatsNavMesh(Enemy enemy, EnemyStaticData enemyStaticData)
//     {
//         var stats = enemy.GetComponent<NavMeshAgent>();
//         stats.speed = enemyStaticData.Speed;
//         stats.stoppingDistance = enemyStaticData.EffectiveDistance;
//     }
//
//     public void Cleanup()
//     {
//         ProgressReaders.Clear();
//         ProgressWriters.Clear();
//     }
// }