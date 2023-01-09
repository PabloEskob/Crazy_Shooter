using Source.Infrastructure;
using Source.Scripts.StaticData;

public interface IStaticDataService : IService
{
    void LoadEnemy();
    EnemyStaticData ForEnemy(MonsterTypeId typeId);
    LevelConfig ForLevel(int levelIndex);
}