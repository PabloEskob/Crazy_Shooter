using System;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private List<EnemySpawner> _enemySpawners;
    [SerializeField] private float _time;

    private LaunchingWaves _launchingWaves;

    private bool _clear;

    public float Time
    {
        get => _time;
        set => _time = value;
    }

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