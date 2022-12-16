using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    [SerializeField] private List<EnemySpawner> _enemySpawners;
    [SerializeField] private float _startFirstWave;

    private StaticDataService _staticDataEnemy;
    private GameFactory _gameFactory;
    private AssetProvider _assetProvider;
    private int _number;
    private LaunchingWaves _launchingWaves;

    private void OnEnable()
    {
        foreach (var enemySpawner in _enemySpawners)
        {
            enemySpawner.OnTurnedSpawner += _launchingWaves.TurnOnSpawn;

            if (enemySpawner.TriggerSpawn != null)
            {
                enemySpawner.TriggerSpawn.TriggerEnter += _launchingWaves.TurnOnSpawn;
            }
        }
    }

    private void OnDisable()
    {
        foreach (var enemySpawner in _enemySpawners)
        {
            enemySpawner.OnTurnedSpawner -= _launchingWaves.TurnOnSpawn;

            if (enemySpawner.TriggerSpawn != null)
            {
                enemySpawner.TriggerSpawn.TriggerEnter -= _launchingWaves.TurnOnSpawn;
            }
        }
    }

    private void Awake()
    {
        _assetProvider = new AssetProvider();
        _staticDataEnemy = new StaticDataService();
        _gameFactory = new GameFactory(_staticDataEnemy, _assetProvider);
        _launchingWaves = new LaunchingWaves(_enemySpawners);
        InitGameWorld();
    }

    private void Start() => StartCoroutine(_launchingWaves.StartFirstWave(_startFirstWave));

    private void InitGameWorld()
    {
        _gameFactory.CreateCar();
        
        for (var i = 0; i < _enemySpawners.Count; i++)
        {
            var enemySpawner = _enemySpawners[i];
            enemySpawner.Init(_gameFactory);
            enemySpawner.SetNumber(i);
        }
    }
}