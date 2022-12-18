using System;

[Serializable]
public class State
{
    public float CurrentHp { get; set; }
    public float MaxHp { get; set; }

    public void ResetHp() => CurrentHp = MaxHp;
}