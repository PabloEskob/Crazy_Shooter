using System.Collections.Generic;
using Agava.YandexGames;
using Dreamteck.Splines;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    [SerializeField] private List<EnemySpawner> _enemySpawners;
    [SerializeField] private float _startFirstWave;
    [SerializeField] private int _hpPlayer;
    [SerializeField] private ActorUI _actorUI;
    [SerializeField] private PlayerRespawn _playerRespawn;
    [SerializeField] private SplineComputer _splineComputer;

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
        var player = _gameFactory.CreatePlayer(_playerRespawn.transform);
        PlayerConstruct(player);
        InitUI(player);
        FillInEnemySpawner();
    }

    private void FillInEnemySpawner()
    {
        for (var i = 0; i < _enemySpawners.Count; i++)
        {
            var enemySpawner = _enemySpawners[i];
            enemySpawner.Init(_gameFactory);
            enemySpawner.SetNumber(i);
        }
    }

    private void PlayerConstruct(Player player)
    {
        player.PlayerHealth.LoadProgress(NewProgress());
        player.PlayerMove.Construct(_splineComputer);
    }

    private void InitUI(Player player)
    {
        _actorUI.Construct(player.PlayerHealth);
    }

    private PlayerProgress NewProgress()
    {
        var progress = new PlayerProgress();
        progress.CarState.MaxHp = _hpPlayer;

        progress.CarState.ResetHp();
        return progress;
    }
}