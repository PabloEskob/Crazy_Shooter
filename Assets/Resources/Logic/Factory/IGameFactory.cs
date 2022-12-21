using System;
using System.Collections.Generic;
using Source.Infrastructure;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

public interface IGameFactory : IService
{
    Enemy CreateEnemy(MonsterTypeId monsterTypeId, Transform parent);
    
    Player Player { get; }
    
    List<ISavedProgressReader> ProgressReaders { get; }
    List<ISavedProgress> ProgressWriters { get; }
    void Cleanup();

}