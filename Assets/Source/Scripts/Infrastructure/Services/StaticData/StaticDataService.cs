using System.Collections.Generic;
using System.Linq;
using Source.Scripts.StaticData;
using UnityEngine;

public class StaticDataService : IStaticDataService
{
    private Dictionary<MonsterTypeId, EnemyStaticData> _enemies;
    private List<LevelConfig> _levelConfigs;

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

    public LevelConfig ForLevel(int levelIndex) => 
        levelIndex >= 0 && levelIndex < _levelConfigs.Count
        ? _levelConfigs[levelIndex]
        : null;
}