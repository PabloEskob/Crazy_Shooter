using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(EnemyAnimator))]
[RequireComponent(typeof(EnemyHealth))]
public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private ParticleSystem _deathFx;
    [SerializeField] private float _timeDied = 5f;
    [SerializeField] private EnemyHealth _enemyHealth;
    [SerializeField] private EnemyAnimator _enemyAnimator;

    private CapsuleCollider _collider;

    public bool IsDied { get; private set; }

    public event Action Happened;

    private void Start()
    {
        _enemyHealth.HealthChanged += OnHealthChanged;
        _collider = GetComponent<CapsuleCollider>();
    }

    private void OnHealthChanged()
    {
        if (_enemyHealth.Max <= 0)
            Die();
    }

    private void Die()
    {
        _enemyHealth.HealthChanged -= OnHealthChanged;
        IsDied = true;
        _collider.enabled = false;

        _enemyAnimator.PlayDeath();
        _deathFx.Play();

        StartCoroutine(DestroyTimer());

        Happened?.Invoke();
    }

    private IEnumerator DestroyTimer()
    {
        var newWaitForSeconds = new WaitForSeconds(_timeDied);
        yield return newWaitForSeconds;
        gameObject.SetActive(false);
    }
}