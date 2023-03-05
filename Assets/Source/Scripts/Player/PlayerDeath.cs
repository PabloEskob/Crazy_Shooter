using System;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
public class PlayerDeath : MonoBehaviour
{
    private Player _player;

    public bool IsDead { get; private set; }

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
        OnDied?.Invoke();
        
    }
}