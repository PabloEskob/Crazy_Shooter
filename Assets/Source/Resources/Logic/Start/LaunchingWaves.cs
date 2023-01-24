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
        Debug.Log($" волны закончились , Начало новой зоны   ");
    }

    public void StartWave()
    {
        foreach (var enemySpawner in _enemySpawners)
        {
            Debug.Log($"Запуск новой волны ");
            enemySpawner.TurnOnEnemy();
        }
    }
}