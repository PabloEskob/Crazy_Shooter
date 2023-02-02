using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LaunchingWaves
{
    private readonly List<EnemySpawner> _enemySpawners;

    public event Action OnEnded;
    public event Action<EnemySpawner> OnNextWave;

    public LaunchingWaves(List<EnemySpawner> enemySpawners) =>
        _enemySpawners = enemySpawners;

    public void TurnOnSpawn()
    {
        var firstOrDefault = _enemySpawners.FirstOrDefault(e => e.Clear == false);

        if (firstOrDefault != null)
        {
            OnNextWave?.Invoke(firstOrDefault);
            return;
        }

        OnEnded?.Invoke();
    }

    public void StartWave()
    {
        foreach (var enemySpawner in _enemySpawners)
            enemySpawner.TurnOnEnemy();
    }
}