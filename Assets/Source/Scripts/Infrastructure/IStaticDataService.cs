using Source.Infrastructure;
using Source.Scripts.StaticData;

namespace Source.Scripts.Infrastructure
{
    public interface IStaticDataService : IService
    {
        LevelConfig ForLevel(int levelIndex);
    }
}