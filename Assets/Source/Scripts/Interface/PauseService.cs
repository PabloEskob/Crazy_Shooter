﻿using System.Collections.Generic;

public class PauseService : IPauseHandler
{
    private readonly List<IPauseHandler> _handlers = new();

    public bool IsPaused { get; private set; }

    public void Register(IPauseHandler handler)
    {
        _handlers.Add(handler);
    }

    public void UnRegister(IPauseHandler handler)
    {
        _handlers.Remove(handler);
    }

    public void SetPaused(bool isPaused)
    {
        IsPaused = isPaused;

        foreach (var handler in _handlers)
            handler.SetPaused(isPaused);
    }
}