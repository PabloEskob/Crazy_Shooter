using System.Collections.Generic;
using Source.Infrastructure;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

public interface IGameFactory : IService
{
    Player CreatePlayer(GameObject initialPoint);
    public void CreateHUD(Player player);
    public void CreateStartScene();
    public void CreateLevelStateMachine(Player player);
    Enemy CreateEnemy(MonsterTypeId monsterTypeId, Transform parent,bool move,EnemySpawner enemySpawner);
    Player Player { get; }
    
    List<ISavedProgressReader> ProgressReaders { get; }
    List<ISavedProgress> ProgressWriters { get; }
    void Cleanup();

}