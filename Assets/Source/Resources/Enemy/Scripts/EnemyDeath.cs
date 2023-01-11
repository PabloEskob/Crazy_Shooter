﻿using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimator))]
[RequireComponent(typeof(EnemyHealth))]
public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private float _timeDied = 5f;
    [SerializeField] private EnemyHealth _enemyHealth;
    [SerializeField] private EnemyAnimator _enemyAnimator;
    [SerializeField] private Waypoint_Indicator _waypointIndicator;

    public bool IsDied { get; private set; }

    public event Action Happened;

    private void Start() =>
        _enemyHealth.HealthChanged += OnHealthChanged;

    private void OnHealthChanged()
    {
        if (_enemyHealth.Max <= 0)
            Die();
    }

    private void Die()
    {
        _enemyHealth.HealthChanged -= OnHealthChanged;
        IsDied = true;

        _enemyAnimator.PlayDeath();
        _waypointIndicator.enabled = false;
        _enemyHealth.Effects.PlayDeath();

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