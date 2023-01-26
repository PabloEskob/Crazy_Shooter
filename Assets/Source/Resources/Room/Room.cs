using System;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private List<EnemySpawner> _enemySpawners;

    private TurningPoint _turningPoint;
    private Player _player;
    private LaunchingWaves _launchingWaves;
    private bool _clear;

    public LaunchingWaves LaunchingWaves => _launchingWaves;
    public int Number { get; set; }

    public event Action OnRoomCleared;

    private void OnDisable()
    {
        foreach (var enemySpawner in _enemySpawners)
            enemySpawner.OnTurnedSpawner -= _launchingWaves.TurnOnSpawn;

        _launchingWaves.Ended -= LaunchingWavesOnEnded;
    }

    private void Awake()
    {
        _launchingWaves = new LaunchingWaves(_enemySpawners);
    }

    private void Start()
    {
        _turningPoint = GetComponentInChildren<TurningPoint>();

        foreach (var enemySpawner in _enemySpawners)
            enemySpawner.OnTurnedSpawner += _launchingWaves.TurnOnSpawn;

        _launchingWaves.Ended += LaunchingWavesOnEnded;
    }

    public TurningPoint GetTurningPoint() =>
        _turningPoint != null ? _turningPoint : null;

    private void LaunchingWavesOnEnded() =>
        OnRoomCleared?.Invoke();

    public void FillInEnemySpawner(IGameFactory gameFactory)
    {
        for (var i = 0; i < _enemySpawners.Count; i++)
        {
            var enemySpawner = _enemySpawners[i];
            enemySpawner.Construct(gameFactory);
            enemySpawner.SetNumber(i);
        }
    }
}