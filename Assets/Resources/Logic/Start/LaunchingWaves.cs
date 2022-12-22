using System;
using System.Collections;
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

        if (firstOrDefault != null && firstOrDefault.Released == false && firstOrDefault.TriggerSpawn == null)
            firstOrDefault.TurnOnEnemy();
        else
            Ended?.Invoke();
    }

    public void TurnOnSpawn(Collider collider, int count)
    {
        var firstOrDefault = _enemySpawners.FirstOrDefault(e => e.Number == count);

        if (firstOrDefault != null)
            firstOrDefault.TurnOnEnemy();
        else
            Ended?.Invoke();
    }

    public IEnumerator StartWave(float startFirstWave)
    {
        var newWaitForSecond = new WaitForSeconds(startFirstWave);
        yield return newWaitForSecond;
        _enemySpawners[0].TurnOnEnemy();
    }
}