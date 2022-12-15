using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StaticDataService : IStaticDataService
{
    private Dictionary<MonsterTypeId, EnemyStaticData> _enemies;

    public StaticDataService()
    {
        LoadEnemy();
    }

    public void LoadEnemy() =>
        _enemies = Resources
            .LoadAll<EnemyStaticData>(AssetPath.PathEnemy)
            .ToDictionary(x => x.MonsterTypeId, x => x);

    public EnemyStaticData ForEnemy(MonsterTypeId typeId)
    {
        return _enemies.TryGetValue(typeId, out EnemyStaticData staticData)
            ? staticData
            : null;
    }
}