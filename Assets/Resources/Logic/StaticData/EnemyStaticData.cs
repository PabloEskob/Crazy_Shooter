using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "StaticData/Enemy")]
public class EnemyStaticData : ScriptableObject
{
    [Range(1, 5)] [SerializeField] private int _hp;

    [Range(1, 5)] [SerializeField] private int _damage;

    [Range(5, 20)] [SerializeField] private int _effectiveDistance;

    [Range(5, 20)] [SerializeField] private float _speed;

    [SerializeField] private MonsterTypeId _monsterTypeId;

    [SerializeField] private Enemy _prefab;

    public float Speed => _speed;
    public MonsterTypeId MonsterTypeId => _monsterTypeId;
    public int Hp => _hp;
    public int Damage => _damage;
    public int EffectiveDistance => _effectiveDistance;
    public Enemy Prefab => _prefab;
}