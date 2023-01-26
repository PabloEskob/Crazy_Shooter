using System;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    private Player _player;

    public bool IsDead { get; set; }

    public event Action OnDied;

    private void Awake() => 
        _player = GetComponent<Player>();

    private void OnDisable() =>
        _player.PlayerHealth.HealthChanged -= OnHealthChanged;

    private void Start() =>
        _player.PlayerHealth.HealthChanged += OnHealthChanged;

    private void OnHealthChanged()
    {
        if (!IsDead && _player.PlayerHealth.Current <= 0)
            Die();
    }

    private void Die()
    {
        IsDead = true;
        // _particleSystem.Play();
        OnDied?.Invoke();
        _player.PlayerAnimator.PlayDeath();
    }
}