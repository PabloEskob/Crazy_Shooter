using System;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimator))]
public class EnemyHealth : MonoBehaviour, IHealth
{
    [SerializeField] private EnemyAnimator _enemyAnimator;
    [SerializeField] private ParticleSystem _particleSystem;
    
    public float Current { get; set; }
    public float Max { get; set; }

    public event Action HealthChanged;

    private void OnCollisionEnter(Collision collision)
    {
        TakeDamage(1);
    }
    
    public void TakeDamage(int damage)
    {
        Max -= damage;
        _particleSystem.Play();
       // _enemyAnimator.PlayHit();
        HealthChanged?.Invoke();
    }
}