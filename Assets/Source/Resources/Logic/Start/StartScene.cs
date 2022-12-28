using Source.Infrastructure;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    [SerializeField] private int _hpPlayer;
    [SerializeField] private ActorUI _actorUI;
    [SerializeField] private LaunchRoom _launchRoom;
    [SerializeField] private Movement _movement;

    private StaticDataService _staticDataEnemy;
    private IGameFactory _gameFactory;
    private IAssetProvider _assetProvider;
    private int _number;

    public void Construct(IGameFactory gameFactory, LaunchRoom launchRoom)
    {
        _gameFactory = gameFactory;
        _launchRoom = launchRoom;
        _launchRoom.Allowed += OnAllowed;
        InitGameWorld();
        StartGame();
    }

    private void OnDisable() =>
        _launchRoom.Allowed -= OnAllowed;

    private void Awake()
    {
        _assetProvider = AllServices.Container.Single<IAssetProvider>();
        _staticDataEnemy = new StaticDataService();
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
}