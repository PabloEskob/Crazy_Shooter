using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieKillCounter : MonoBehaviour
{
    [SerializeField] private EnemySpawner[] _enemySpawners;
    [SerializeField] private int _maxZombieQuantity;

    private GameStatusScreen _statusScreen;

    public int ZombieKilledQuantity { get; private set; } = 0;
    public int MaxZombieQuantity => _maxZombieQuantity;

    public event Action QuantityChanged;

    private void OnEnable()
    {
        foreach (EnemySpawner spawner in _enemySpawners)
            spawner.EnemyDied += OnEnemyDied;
    }

    private void OnDisable()
    {
        foreach (EnemySpawner spawner in _enemySpawners)
            spawner.EnemyDied += OnEnemyDied;
    }

    private void OnEnemyDied() => Add();

    private void Start() => 
        _statusScreen = FindObjectOfType<GameStatusScreen>();

    private void Add()
    {
        if (ZombieKilledQuantity < _maxZombieQuantity)
            ZombieKilledQuantity++;

        if (ZombieKilledQuantity == _maxZombieQuantity)
        {
            _statusScreen = FindObjectOfType<GameStatusScreen>();
            _statusScreen.PlayerVictory();
        }

        QuantityChanged?.Invoke();
    }
}