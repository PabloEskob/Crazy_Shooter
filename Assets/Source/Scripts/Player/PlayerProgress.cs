using System;
using Source.Scripts.Data;

[Serializable]
public class PlayerProgress
{
    public State CarState { get; set; }
    public WorldData WorldData;

    public PlayerProgress()
    {
        CarState = new State();
    }

    public PlayerProgress(string initialLevel)
    {
        WorldData = new WorldData(initialLevel);
    }
}