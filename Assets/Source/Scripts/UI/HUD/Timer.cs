using System;

public class Timer : ITimer
{
    public event Action Started;
    public event Action Stopped;
    public event Action Completed;
    public event Action Updated;

    public float TotalTime { get; private set; }
    public float TimeLeft { get; private set; }
    public bool IsTimerStart { get; private set; } = false;
            
    public void Tick(float tick)
    {
        if (TimeLeft == 0)
            return;

        TimeLeft -= tick;
        Updated?.Invoke();

        if (TimeLeft <= 0)
        {
            TimeLeft = 0;
            Completed?.Invoke();
        }
    }

    public void Start(float time)
    {
        TotalTime = time;
        TimeLeft = TotalTime;
        IsTimerStart = true;
        Started?.Invoke();
    }

    public void Stop()
    {
        TimeLeft = 0;
        IsTimerStart = false;
        Stopped?.Invoke();
    }

    public void AddTime(float additionalTime) => 
        TimeLeft += additionalTime;
}

public interface ITimer
{
    public abstract event Action Started;
    public abstract event Action Stopped;
    public abstract event Action Completed;
    public abstract event Action Updated;

    public abstract float TotalTime { get; }
    public abstract float TimeLeft { get; }
}