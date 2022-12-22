using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LaunchingWaves
{
    private readonly List<EnemySpawner> _enemySpawners;

    public LaunchingWaves(List<EnemySpawner> enemySpawners)
    {
        _enemySpawners = enemySpawners;
    }

    public void TurnOnSpawn()
    {
        var firstOrDefault = _enemySpawners.FirstOrDefault(e => e.Clear == false);

        if (firstOrDefault != null && firstOrDefault.Released == false && firstOrDefault.TriggerSpawn == null)
        {
            firstOrDefault.TurnOnEnemy();
        }
    }

    public void TurnOnSpawn(Collider collider, int count)
    {
        var firstOrDefault = _enemySpawners.FirstOrDefault(e => e.Number == count);

        if (firstOrDefault != null) firstOrDefault.TurnOnEnemy();
    }


    public IEnumerator StartFirstWave(float startFirstWave)
    {
        var newWaitForSecond = new WaitForSeconds(startFirstWave);
        yield return newWaitForSecond;
        _enemySpawners[0].TurnOnEnemy();
    }
}