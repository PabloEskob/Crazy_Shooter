using System;
using Source.Infrastructure;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    [SerializeField] private Movement _movement;

    private GameStatusScreen _gameStatusScreen;
    private LaunchRoom _launchRoom;
    private StaticDataService _staticDataEnemy;
    private IGameFactory _gameFactory;
    private IAssetProvider _assetProvider;
    private int _number;
    private FinishLevel _finishLevel;

    private void OnDisable()
    {
       // _launchRoom.EndedRoom -= LaunchVictoryScreen;
        _finishLevel.EndedLevel -= LaunchVictoryScreen;
    }

    public void Construct(IGameFactory gameFactory, LaunchRoom launchRoom, GameStatusScreen gameStatusScreen,
        FinishLevel finishLevel)
    {
        _gameStatusScreen = gameStatusScreen;
        _gameFactory = gameFactory;
        _launchRoom = launchRoom;
        _finishLevel = finishLevel;
        _finishLevel.EndedLevel += LaunchVictoryScreen;
        // _launchRoom.EndedRoom += LaunchVictoryScreen;
        InitGameWorld();
        StartGame();
    }

    public void LaunchVictoryScreen()
    {
        _gameStatusScreen.PlayerVictory();
    }

    private void Awake()
    {
        _assetProvider = AllServices.Container.Single<IAssetProvider>();
        _staticDataEnemy = new StaticDataService();
    }

    private void InitGameWorld() =>
        _launchRoom.Fill(_gameFactory);

    private void StartGame() =>
        _launchRoom.StartFirstRoom();
}