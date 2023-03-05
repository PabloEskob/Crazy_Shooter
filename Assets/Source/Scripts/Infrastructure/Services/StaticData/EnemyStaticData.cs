using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "StaticData/Enemy")]
public class EnemyStaticData : ScriptableObject
{
    [SerializeField] private MonsterTypeId _monsterTypeId;
    [SerializeField] private  List<Enemy> _prefabs;

    public MonsterTypeId MonsterTypeId => _monsterTypeId;
    public Enemy Prefab
    {
        get
        {
            var value = Random.Range(0, _prefabs.Count);
            return _prefabs[value];
        }
    }
}