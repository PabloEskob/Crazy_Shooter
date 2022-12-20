using System;
using System.Collections.Generic;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

public interface IGameFactory
{
    Enemy CreateEnemy(MonsterTypeId monsterTypeId, Transform parent);
    
    Player Player { get; }

}