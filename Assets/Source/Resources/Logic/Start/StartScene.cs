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

    private void OnDisable() => 
        _launchRoom.EndedRoom -= LaunchVictoryScreen;

    public void Construct(IGameFactory gameFactory, LaunchRoom launchRoom, GameStatusScreen gameStatusScreen)
    {
        _gameStatusScreen = gameStatusScreen;
        _gameFactory = gameFactory;
        _launchRoom = launchRoom;
        _launchRoom.EndedRoom += LaunchVictoryScreen;
        InitGameWorld();
        StartGame();
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
    
    private void LaunchVictoryScreen() =>
        _gameStatusScreen.PlayerVictory();
}