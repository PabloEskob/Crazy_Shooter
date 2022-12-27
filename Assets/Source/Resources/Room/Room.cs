using System;
using System.Collections.Generic;
using Source.Scripts.Infrastructure.Factory;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private List<EnemySpawner> _enemySpawners;
    [SerializeField] private float _delayWave;
    [SerializeField] private float _startThisRoom;

    private LaunchingWaves _launchingWaves;

    private bool _clear;

    public float DelayWave => _delayWave;
    public float StartThisRoom => _startThisRoom;

    public LaunchingWaves LaunchingWaves => _launchingWaves;
    public int Number { get; set; }

    public event Action<int> StartedNewRoom;

    private void OnDisable()
    {
        foreach (var enemySpawner in _enemySpawners)
        {
            enemySpawner.OnTurnedSpawner -= _launchingWaves.TurnOnSpawn;

            if (enemySpawner.TriggerSpawn != null)
                enemySpawner.TriggerSpawn.TriggerEnter -= _launchingWaves.TurnOnSpawn;
        }

        _launchingWaves.Ended -= LaunchingWavesOnEnded;
    }

    private void Awake() =>
        _launchingWaves = new LaunchingWaves(_enemySpawners);

    private void Start()
    {
        foreach (var enemySpawner in _enemySpawners)
        {
            enemySpawner.OnTurnedSpawner += _launchingWaves.TurnOnSpawn;

            if (enemySpawner.TriggerSpawn != null)
                enemySpawner.TriggerSpawn.TriggerEnter += _launchingWaves.TurnOnSpawn;
        }

        _launchingWaves.Ended += LaunchingWavesOnEnded;
    }

    private void LaunchingWavesOnEnded() =>
        StartedNewRoom?.Invoke(Number);


    public void FillInEnemySpawner(GameFactory gameFactory)
    {
        for (var i = 0; i < _enemySpawners.Count; i++)
        {
            var enemySpawner = _enemySpawners[i];
            enemySpawner.Init(gameFactory);
            enemySpawner.SetNumber(i);
        }
    }
}