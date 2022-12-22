using UnityEngine;

public class ActorUI : MonoBehaviour
{
    [SerializeField] private HpBar _hpBar;

    private PlayerHealth _playerHealth;
    private ButtonForward _buttonForward;
    private PlayerMove _playerMove;

    public ButtonForward ButtonForward => _buttonForward;

    private void OnEnable()
    {
        _buttonForward.OnClick += OnClick;
        _buttonForward.Moved += CanMoved;
    }
    
    private void OnDisable()
    {
        _playerHealth.HealthChanged -= UpdateHpBar;
        _buttonForward.OnClick -= OnClick;
        _playerMove.Stopped -= PlayerMoveOnStopped;
        _buttonForward.Moved -= CanMoved;
    }

    private void Awake() =>
        _buttonForward = GetComponentInChildren<ButtonForward>();

    public void Construct(PlayerHealth playerHealth)
    {
        _playerHealth = playerHealth;
        _playerMove = playerHealth.GetComponent<PlayerMove>();
        _playerHealth.HealthChanged += UpdateHpBar;
        _playerMove.Stopped += PlayerMoveOnStopped;
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