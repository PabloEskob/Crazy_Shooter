﻿namespace Source.Scripts.Infrastructure.Services.PersistentProgress
{
    public interface ISavedProgressReader
    {
        void LoadProgress(PlayerProgress progress);
    }
}