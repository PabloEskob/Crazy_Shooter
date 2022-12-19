using System;
using System.Collections.Generic;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
using Source.Scripts.Infrastructure.Services.SaveLoad;
using UnityEngine;

public interface IGameFactory
{
    Enemy CreateEnemy(MonsterTypeId monsterTypeId, Transform parent);
    
    Player Player { get; }
    List<ISavedProgressReader> ProgressReaders { get; }
    List<ISavedProgress> ProgressWriters { get; }
    void Cleanup();

}