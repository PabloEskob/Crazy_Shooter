public interface IStaticDataService
{
    void LoadEnemy();
    EnemyStaticData ForEnemy(MonsterTypeId typeId);
}