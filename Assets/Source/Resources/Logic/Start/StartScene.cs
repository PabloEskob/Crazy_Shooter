using Agava.YandexGames;
using Dreamteck.Splines;
using Source.Infrastructure;
using UnityEngine;

[RequireComponent(typeof(LaunchRoom))]
public class StartScene : MonoBehaviour
{
    [SerializeField] private int _hpPlayer;
    [SerializeField] private ActorUI _actorUI;
    [SerializeField] private PlayerRespawn _playerRespawn;
    [SerializeField] private SplineComputer _splineComputer;
    [SerializeField] private LaunchRoom _launchRoom;
    [SerializeField] private Movement _movement;

    private StaticDataService _staticDataEnemy;
    private GameFactory _gameFactory;
    private IAssetProvider _assetProvider;
    private int _number;

    private void OnEnable() =>
        _launchRoom.Allowed += OnAllowed;

    private void OnDisable() =>
        _launchRoom.Allowed -= OnAllowed;

    private void Awake()
    {
        _assetProvider = AllServices.Container.Single<IAssetProvider>();
        _staticDataEnemy = new StaticDataService();
        _gameFactory = new GameFactory(_staticDataEnemy, _assetProvider);
        InitGameWorld();
    }

    private void Start() =>
        StartGame();

    private void InitGameWorld()
    {
        var player = _gameFactory.CreatePlayer();
        PlayerConstruct(player);
        InitUI(player);
        _launchRoom.Fill(_gameFactory);
    }

    private void PlayerConstruct(Player player)
    {
        player.PlayerHealth.LoadProgress(NewProgress());
        player.PlayerMove.Construct(_splineComputer);
    }

    private void InitUI(Player player) =>
        _actorUI.Construct(player.PlayerHealth);

    private PlayerProgress NewProgress()
    {
        var progress = new PlayerProgress();
        progress.CarState.MaxHp = _hpPlayer;
        progress.CarState.ResetHp();
        return progress;
    }

    private void StartGame() =>
        _launchRoom.StartFirstRoom();

    private void OnAllowed()
    {
        if (_movement == Movement.Move)
            _actorUI.ButtonForward.SwitchOn();
    }
}