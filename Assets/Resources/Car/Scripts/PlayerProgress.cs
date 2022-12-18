using System;

[Serializable]
public class PlayerProgress
{
    public State CarState { get; set; }

    public PlayerProgress()
    {
        CarState = new State();
    }
}