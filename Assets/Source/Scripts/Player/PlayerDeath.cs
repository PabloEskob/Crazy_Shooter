using System;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private PlayerAnimator _animator;
    [SerializeField] private ParticleSystem _particleSystem;

    public bool IsDead { get; set; }

    public event Action OnDied;

    private void OnDisable() =>
        _playerHealth.HealthChanged -= OnHealthChanged;

    private void Start() =>
        _playerHealth.HealthChanged += OnHealthChanged;

    private void OnHealthChanged()
    {
        if (!IsDead && _playerHealth.Current <= 0)
            Die();
    }

    private void Die()
    {
        IsDead = true;
        // _particleSystem.Play();
        OnDied?.Invoke();
        _animator.PlayDeath();
    }
}