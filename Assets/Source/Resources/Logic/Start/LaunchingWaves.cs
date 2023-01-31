using System;
using System.Collections.Generic;
using System.Linq;

public class LaunchingWaves
{
    private readonly List<EnemySpawner> _enemySpawners;

    public event Action Ended;
    public event Action NextWave;

    public LaunchingWaves(List<EnemySpawner> enemySpawners) =>
        _enemySpawners = enemySpawners;

    public void TurnOnSpawn()
    {
        var firstOrDefault = _enemySpawners.FirstOrDefault(e => e.Clear == false);
        
        if (firstOrDefault != null)
        {
            NextWave?.Invoke();
            return;
        }
        
        Ended?.Invoke();
    }

    public void StartWave()
    {
        foreach (var enemySpawner in _enemySpawners) 
            enemySpawner.TurnOnEnemy();
    }
}