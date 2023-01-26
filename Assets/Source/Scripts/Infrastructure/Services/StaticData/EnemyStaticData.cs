using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "StaticData/Enemy")]
public class EnemyStaticData : ScriptableObject
{
    [SerializeField] private MonsterTypeId _monsterTypeId;
    [SerializeField] private Enemy _prefab;

    public MonsterTypeId MonsterTypeId => _monsterTypeId;
    public Enemy Prefab => _prefab;
}