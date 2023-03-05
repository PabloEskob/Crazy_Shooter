using Source.Scripts.Data;

namespace Source.Scripts.Infrastructure.Services.PersistentProgress
{
    public interface ISavedProgress : ISavedProgressReader
    {
        void UpdateProgress(PlayerProgress progress);
    }

    public interface ISavedProgressReader
    {
        void LoadProgress(PlayerProgress progress);
    }
}