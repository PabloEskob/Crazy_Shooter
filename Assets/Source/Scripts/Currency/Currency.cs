using System;
using UnityEngine;

public class Currency 
{
    [SerializeField] private int _value;

    public event Action Changed;

    public int Value => _value;

    public void Add(int value)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException(nameof(value));

        _value += value;
        Changed?.Invoke();
    }

    public void Spend(int value)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException(nameof(value));

        _value -= value;

        if (_value < 0)
            _value = 0;

        Changed?.Invoke();
    }
}