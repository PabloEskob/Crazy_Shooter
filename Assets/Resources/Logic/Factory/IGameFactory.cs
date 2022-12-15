using UnityEngine;

public interface IGameFactory
{
    Enemy CreateEnemy(MonsterTypeId monsterTypeId, Transform parent);
}