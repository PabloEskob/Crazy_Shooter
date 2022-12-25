using System;
using InfimaGames.LowPolyShooterPack.Legacy;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimator))]
public class EnemyHealth : MonoBehaviour, IHealth
{
    [SerializeField] private EnemyAnimator _enemyAnimator;
    [SerializeField] private ParticleSystem _particleSystem;

    private EnemyMove _enemyMove;
    private bool _canPlayHit = true;

    public float Current { get; set; }
    public float Max { get; set; }

    public event Action HealthChanged;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Projectile>())
            TakeDamage(1);
    }

    private void Start()
    {
        _enemyMove = GetComponent<EnemyMove>();
    }

    public void OnHitEnded()
    {
        _canPlayHit = true;
        _enemyMove.ContinueMove();
    }

    public void OnStartHit()
    {
        _canPlayHit = false;
        _enemyMove.StopMove();
    }

    public void TakeDamage(int damage)
    {
        Max -= damage;

        if (_canPlayHit)
            _enemyAnimator.PlayHit();

        //_particleSystem.Play();
        HealthChanged?.Invoke();
    }
}