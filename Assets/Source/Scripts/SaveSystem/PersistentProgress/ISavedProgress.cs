using Source.Scripts.Data;

namespace Source.Scripts.Infrastructure.Services.PersistentProgress
{
    public interface ISavedProgress : ISavedProgressReader
    {
        void UpdateProgress(IStorage storage);
    }

    public interface ISavedProgressReader
    {
        void LoadProgress(IStorage storage);
    }
}