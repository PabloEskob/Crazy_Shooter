using System;
using UnityEngine;

public interface IGameFactory
{
    Enemy CreateEnemy(MonsterTypeId monsterTypeId, Transform parent);
    
    Player Player { get; }
    
}