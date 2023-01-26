using System.Collections.Generic;
using System.Linq;
using Source.Scripts.StaticData;
using UnityEngine;

public class StaticDataService : IStaticDataService
{
    private const string GameDataPath = "GameData/GameConfig";

    private Dictionary<MonsterTypeId, EnemyStaticData> _enemies;
    private List<LevelNames> _levelConfigs;

    private GameConfig _gameConfig;

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

    public LevelNames ForLevel(int levelIndex) => 
        levelIndex >= 0 && levelIndex < _gameConfig.LevelNames.Length
        ? _gameConfig.LevelNames[levelIndex]
        : null;

    public void LoadGameConfig() => 
        _gameConfig = Resources.Load<GameConfig>(GameDataPath);

    public GameConfig GetGameConfig() => _gameConfig;
}