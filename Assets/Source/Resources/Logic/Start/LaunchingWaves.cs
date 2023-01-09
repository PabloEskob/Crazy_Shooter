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

        if (firstOrDefault == null)
        {
            Ended?.Invoke();
            Debug.Log($" волны закончились , Начало новой зоны   ");
        }
    }

    /*public void TurnOnSpawn()
    {
        var firstOrDefault = _enemySpawners.FirstOrDefault(e => e.Clear == false);

        if (firstOrDefault != null && firstOrDefault.Released == false && firstOrDefault.TriggerSpawn == null)
            firstOrDefault.TurnOnEnemy();
        else
        {
            Ended?.Invoke();
            Debug.Log($" волна закончилась , пристукпаю к новой комнате  ");
        }
    }*/

    public void TurnOnSpawn(Collider collider, int count)
    {
        var firstOrDefault = _enemySpawners.FirstOrDefault(e => e.Number == count);

        if (firstOrDefault != null)
            firstOrDefault.TurnOnEnemy();
        else
        {
            Debug.Log($" волна закончилась , пристукпаю к новой комнате  ");
            Ended?.Invoke();
        }
    }

    public IEnumerator StartWave(float startFirstWave, float delay)
    {
        var startNewRoom = new WaitForSeconds(startFirstWave);
        var delayWave = new WaitForSeconds(delay);
        yield return startNewRoom;

        foreach (var enemySpawner in _enemySpawners)
        {
            Debug.Log($"Запуск новой волны ");
            enemySpawner.TurnOnEnemy();
            yield return delayWave;
        }
    }
}