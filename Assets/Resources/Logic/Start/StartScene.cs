using System;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    [SerializeField] private List<EnemySpawner> _enemySpawners;
    [SerializeField] private float _startFirstWave;

    private StaticDataService _staticDataEnemy;
    private GameFactory _gameFactory;
    private AssetProvider _assetProvider;
    private Player _player;
    private int _number;
    private LaunchingWaves _launchingWaves;

    private void OnEnable()
    {
        foreach (var enemySpawner in _enemySpawners) 
            enemySpawner.OnTurnedSpawner += _launchingWaves.TurnOnSpawn;
    }

    private void OnDisable()
    {
        foreach (var enemySpawner in _enemySpawners) 
            enemySpawner.OnTurnedSpawner -= _launchingWaves.TurnOnSpawn;
    }

    private void Awake()
    {
        _assetProvider = new AssetProvider();
        _staticDataEnemy = new StaticDataService();
        _gameFactory = new GameFactory(_staticDataEnemy, _assetProvider);
        _launchingWaves = new LaunchingWaves(_enemySpawners);
        _player = _gameFactory.CreateCar().GetComponent<Player>();
        
        InitGameWorld();
    }

    private void Start() => StartCoroutine(_launchingWaves.StartFirstWave(_startFirstWave));

    private void InitGameWorld()
    {
        for (var index = 0; index < _enemySpawners.Count; index++)
        {
            var enemySpawner = _enemySpawners[index];
            enemySpawner.Init(_gameFactory, _player);
            enemySpawner.SetNumber(index);
        }
    }
}