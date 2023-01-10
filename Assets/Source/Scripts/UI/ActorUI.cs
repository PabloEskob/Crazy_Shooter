using UnityEngine;

public class ActorUI : MonoBehaviour
{
    private const string PlayerTag = "Player";

    [SerializeField] private ProgressBarPro _hpBar;
    [SerializeField] private CanvasGroup _imageRedScreen;

    private PlayerHealth _playerHealth;
    private ButtonForward _buttonForward;
    private PlayerMove _playerMove;
    private GameStatusScreen _gameStatusScreen;

    public ButtonForward ButtonForward => _buttonForward;

    private void OnEnable()
    {
        _buttonForward.OnClick += OnClick;
        _buttonForward.Moved += CanMoved;
    }

    private void OnDisable()
    {
        _buttonForward.OnClick -= OnClick;
        _buttonForward.Moved -= CanMoved;
    }

    private void Awake() =>
        _buttonForward = GetComponentInChildren<ButtonForward>();

    private void Start()
    {
        _playerHealth = GameObject.FindGameObjectWithTag(PlayerTag).GetComponent<PlayerHealth>();
        Construct(_playerHealth);
    }

    private void Construct(PlayerHealth playerHealth)
    {
        _playerHealth = playerHealth;
        _playerMove = playerHealth.GetComponent<PlayerMove>();
        _hpBar.SetMaxHpImage(_playerHealth.Max);
        _hpBar.SetValueImage(_imageRedScreen);
        _playerHealth.HealthChanged += UpdateHpBar;
        _playerHealth.Disabled += OnHealthDisabled;
        _playerMove.Stopped += PlayerMoveOnStopped;
        _playerMove.Disabled += OnMoveDisabled;
    }

    private void OnMoveDisabled()
    {
        _playerMove.Stopped -= PlayerMoveOnStopped;
        _playerMove.Disabled -= OnMoveDisabled;
    }

    private void OnHealthDisabled()
    {
        _playerHealth.HealthChanged -= UpdateHpBar;
        _playerHealth.Disabled -= OnHealthDisabled;
    }

    private void PlayerMoveOnStopped() =>
        _buttonForward.SwitchOff();

    private void OnClick() =>
        _playerMove.PlayMove();

    private void UpdateHpBar() =>
        _hpBar.SetValue(_playerHealth.Current, _playerHealth.Max);

    private void CanMoved() =>
        _playerMove.CanMove = true;
}