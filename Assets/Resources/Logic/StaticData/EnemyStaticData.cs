using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "StaticData/Enemy")]
public class EnemyStaticData : ScriptableObject
{
    [Range(1, 5)] [SerializeField] private int _hp;

    [Range(1, 5)] [SerializeField] private int _damage;

    [Range(3, 20)] [SerializeField] private float _effectiveDistance;

    [Range(5, 20)] [SerializeField] private float _speed;

    [Range(3, 20)] [SerializeField] private float _attackCooldown;


    [SerializeField] private MonsterTypeId _monsterTypeId;

    [SerializeField] private Enemy _prefab;

    public float AttackCooldown => _attackCooldown;
    public float Speed => _speed;
    public MonsterTypeId MonsterTypeId => _monsterTypeId;
    public int Hp => _hp;
    public int Damage => _damage;
    public float EffectiveDistance => _effectiveDistance;
    public Enemy Prefab => _prefab;
}