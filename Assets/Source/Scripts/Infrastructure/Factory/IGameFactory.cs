using System.Collections.Generic;
using Assets.Source.Scripts.Analytics;
using Source.Infrastructure;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

public interface IGameFactory : IService
{
    Player CreatePlayer(GameObject initialPoint);
    public void CreateHUD(Player player);
    public void CreateStartScene();
    public void CreateLevelStateMachine(Player player, IAnalyticManager analyticManager);
    Enemy CreateEnemy(MonsterTypeId monsterTypeId, Vector3 parent,bool move,EnemySpawner enemySpawner);
    Player Player { get; }
    
    List<ISavedProgressReader> ProgressReaders { get; }
    List<ISavedProgress> ProgressWriters { get; }
    void Cleanup();

}