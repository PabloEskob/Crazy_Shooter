using Dreamteck.Splines;
using UnityEngine;

[RequireComponent(typeof(SplineFollower))]
[RequireComponent(typeof(CarHealth))]
public class CarDeath : MonoBehaviour
{
    [SerializeField] private CarHealth _carHealth;
    [SerializeField] private SplineFollower _splineFollower;
    [SerializeField] private CarAnimator _animator;
    [SerializeField] private ParticleSystem _particleSystem;

    private bool _isDead;

    private void OnDisable() =>
        _carHealth.HealthChanged -= OnHealthChanged;

    private void Start() =>
        _carHealth.HealthChanged += OnHealthChanged;

    private void OnHealthChanged()
    {
        if (!_isDead && _carHealth.Current <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _isDead = true;
        _splineFollower.enabled = false;
        _particleSystem.Play();
        _animator.PlayDeath();
    }
}