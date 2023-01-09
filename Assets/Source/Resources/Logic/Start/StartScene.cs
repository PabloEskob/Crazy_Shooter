using System;
using Source.Infrastructure;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    private const string ActorUiTag = "ActorUi";

    [SerializeField] private LaunchRoom _launchRoom;
    [SerializeField] private Movement _movement;

    private ActorUI _actorUI;
    private StaticDataService _staticDataEnemy;
    private IGameFactory _gameFactory;
    private IAssetProvider _assetProvider;
    private int _number;

    public void Construct(IGameFactory gameFactory, LaunchRoom launchRoom)
    {
        _gameFactory = gameFactory;
        _launchRoom = launchRoom;
        _launchRoom.Allowed += OnAllowed;
        _launchRoom.Disabled += OnRoomDisabled;
        _launchRoom.EndedRoom += LaunchVictoryScreen;
        InitGameWorld();
        StartGame();
    }

    private void OnRoomDisabled()
    {
        _launchRoom.Allowed -= OnAllowed;
        _launchRoom.Disabled -= OnRoomDisabled;
        _launchRoom.EndedRoom -= LaunchVictoryScreen;
    }

    private void Awake()
    {
        _assetProvider = AllServices.Container.Single<IAssetProvider>();
        _staticDataEnemy = new StaticDataService();
    }

    private void Start()
    {
        _actorUI = GameObject.FindGameObjectWithTag(ActorUiTag).GetComponent<ActorUI>();
    }

    private void InitGameWorld() =>
        _launchRoom.Fill(_gameFactory);

    private void StartGame() =>
        _launchRoom.StartFirstRoom();

    private void OnAllowed()
    {
        if (_movement == Movement.Move)
            _actorUI.ButtonForward.SwitchOn();
    }

    private void LaunchVictoryScreen()
    {
        _actorUI.VictoryScreen.Show();
    }
}