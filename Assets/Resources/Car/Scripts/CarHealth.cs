using System;
using UnityEngine;

public class CarHealth : MonoBehaviour, IHealth
{
    [SerializeField] private CarAnimator _carAnimator;

    private State _carState;

    public event Action HealthChanged;

    public float Current
    {
        get => _carState.CurrentHp;
        set
        {
            if (_carState.CurrentHp != value)
            {
                _carState.CurrentHp = value;
                HealthChanged?.Invoke();
            }
        }
    }

    public float Max
    {
        get => _carState.MaxHp;
        set => _carState.MaxHp = value;
    }

    public void LoadProgress(PlayerProgress playerProgress) =>
        _carState = playerProgress.CarState;

    public void TakeDamage(int damage)
    {
        if (Current <= 0)
            return;

        Current -= damage;
       _carAnimator.PlayHit();
    }
}