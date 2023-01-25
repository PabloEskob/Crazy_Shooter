using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LaunchingWaves
{
    private readonly List<EnemySpawner> _enemySpawners;

    public event Action Ended;

    public LaunchingWaves(List<EnemySpawner> enemySpawners) =>
        _enemySpawners = enemySpawners;

    public void TurnOnSpawn()
    {
        var firstOrDefault = _enemySpawners.FirstOrDefault(e => e.Clear == false);

        if (firstOrDefault != null) return;
        Ended?.Invoke();
    }

    public void StartWave()
    {
        foreach (var enemySpawner in _enemySpawners) 
            enemySpawner.TurnOnEnemy();
    }
}