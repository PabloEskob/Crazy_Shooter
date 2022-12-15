using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LaunchingWaves
{
    private readonly List<EnemySpawner> _enemySpawners;
    private int _number;

    public LaunchingWaves(List<EnemySpawner> enemySpawners)
    {
        _enemySpawners = enemySpawners;
    }

    public void TurnOnSpawn()
    {
        _number++;

        foreach (var enemySpawner in _enemySpawners.Where(enemySpawner => enemySpawner.Number==_number))
            enemySpawner.TurnOnEnemy();
    }

    public IEnumerator StartFirstWave(float startFirstWave )
    {
        var newWaitForSecond = new WaitForSeconds(startFirstWave);
        yield return newWaitForSecond;
        _enemySpawners[0].TurnOnEnemy();
    }
}