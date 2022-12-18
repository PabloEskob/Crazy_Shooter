using System;
using UnityEngine;

public class CarHealth : MonoBehaviour
{
    [SerializeField] private CarAnimator _carAnimator;

    private State _carState;

    public event Action HealthChanged;

    public float Current
    {
        get => _carState.CurrentHp;
        private set
        {
            if (_carState.CurrentHp != value)
            {
                _carState.CurrentHp = value;
                HealthChanged?.Invoke();
            }
        }
    }

    public float Max => _carState.MaxHp;

    public void LoadProgress(PlayerProgress playerProgress) =>
        _carState = playerProgress.CarState;

    public void TakeDamage(int damage)
    {
        Debug.Log(Current);
        if (Current <= 0)
            return;

        Current -= damage;
        _carAnimator.PlayHit();
    }
}