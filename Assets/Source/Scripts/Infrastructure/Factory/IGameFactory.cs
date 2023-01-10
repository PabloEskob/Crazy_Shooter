using System;
using System.Collections.Generic;
using Source.Infrastructure;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

public interface IGameFactory : IService
{
    
    Player CreatePlayer(GameObject initialPoint);
    public void CreateHUD(Player player);
    public void CreateStartScene();
    Enemy CreateEnemy(MonsterTypeId monsterTypeId, Transform parent);
    Player Player { get; }
    
    List<ISavedProgressReader> ProgressReaders { get; }
    List<ISavedProgress> ProgressWriters { get; }
    void Cleanup();

}