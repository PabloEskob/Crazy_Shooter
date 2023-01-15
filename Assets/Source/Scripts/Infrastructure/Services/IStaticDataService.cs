using Source.Infrastructure;
using Source.Scripts.StaticData;

public interface IStaticDataService : IService
{
    void LoadEnemy();
    void LoadGameConfig();
    GameConfig GetGameConfig();
    EnemyStaticData ForEnemy(MonsterTypeId typeId);
    LevelNames ForLevel(int levelIndex);
}