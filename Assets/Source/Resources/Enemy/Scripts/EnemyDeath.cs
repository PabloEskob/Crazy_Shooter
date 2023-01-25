using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimator))]
[RequireComponent(typeof(EnemyHealth))]
public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private float _timeDied = 5f;

    private Enemy _enemy;
    public event Action OnHappened;
    public bool IsDie { get; private set; }

    private void Awake() =>
        _enemy = GetComponent<Enemy>();

    private void Start() =>
        _enemy.EnemyHealth.HealthChanged += OnHealthChanged;

    private void OnHealthChanged()
    {
        if (_enemy.EnemyHealth.Max <= 0)
            Die();
    }

    private void Die()
    {
        _enemy.EnemyHealth.HealthChanged -= OnHealthChanged;
        IsDie = true;
        _enemy.EnemyStateMachine.Enter<StateOfDeathEnemy>();
        _enemy.WaypointIndicator.enabled = false;
        StartCoroutine(DestroyTimer());
        OnHappened?.Invoke();
    }

    private IEnumerator DestroyTimer()
    {
        var newWaitForSeconds = new WaitForSeconds(_timeDied);
        yield return newWaitForSeconds;
        gameObject.SetActive(false);
    }
}