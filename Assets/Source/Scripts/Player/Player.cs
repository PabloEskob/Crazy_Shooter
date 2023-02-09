using System;
using InfimaGames.LowPolyShooterPack;
using InfimaGames.LowPolyShooterPack.Interface;
using Source.Scripts.PostProcess;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerMove))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerDeath))]
[RequireComponent(typeof(PlayerRotate))]
public class Player : MonoBehaviour
{
    [SerializeField] private PlayerBody _playerBody;
    [SerializeField] private PostProcess _postProcess;

    private PlayerMove _playerMove;
    private PlayerHealth _playerHealth;
    private PlayerAnimator _playerAnimator;
    private PlayerDeath _playerDeath;
    private PlayerRotate _playerRotate;
    private Character _character;
    private ActorUI _actorUI;
    private CanvasSpawner _canvasSpawner;

    public PlayerMove PlayerMove =>
        _playerMove;

    public PlayerHealth PlayerHealth =>
        _playerHealth;

    public PlayerDeath PlayerDeath =>
        _playerDeath;

    public PlayerAnimator PlayerAnimator =>
        _playerAnimator;

    public PlayerRotate PlayerRotate =>
        _playerRotate;

    public Character Character =>
        _character;

    public PostProcess PostProcess =>
        _postProcess;

    public event Action<ActorUI> OnSpawnedActorUi;

    private void Awake()
    {
        _playerMove = GetComponent<PlayerMove>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerHealth = GetComponent<PlayerHealth>();
        _playerDeath = GetComponent<PlayerDeath>();
        _playerRotate = GetComponent<PlayerRotate>();
        _character = GetComponent<Character>();
        _canvasSpawner = GetComponent<CanvasSpawner>();
    }

    private void Start()
    {
#if UNITY_EDITOR
        _actorUI = _canvasSpawner.CurrentCanvas.GetComponent<ActorUI>();

        OnSpawnedActorUi?.Invoke(_actorUI);
#endif

#if !UNITY_EDITOR && UNITY_WEBGL
        _canvasSpawner.Spawned += SetActorUi;
#endif
    }

    public void SetDisable() =>
        _playerBody.Disable();

    private void SetActorUi()
    {
        _actorUI = _canvasSpawner.CurrentCanvas.GetComponent<ActorUI>();
        OnSpawnedActorUi?.Invoke(_actorUI);
        _canvasSpawner.Spawned -= SetActorUi;
    }
}