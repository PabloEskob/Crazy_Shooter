using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private PlayerAnimator _animator;
    [SerializeField] private ParticleSystem _particleSystem;

    private bool _isDead;

    private void OnDisable() =>
        _playerHealth.HealthChanged -= OnHealthChanged;

    private void Start() =>
        _playerHealth.HealthChanged += OnHealthChanged;

    private void OnHealthChanged()
    {
        if (!_isDead && _playerHealth.Current <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _isDead = true;
        _particleSystem.Play();
        _animator.PlayDeath();
    }
}