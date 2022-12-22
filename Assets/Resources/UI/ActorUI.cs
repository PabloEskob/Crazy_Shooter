using UnityEngine;

public class ActorUI : MonoBehaviour
{
    [SerializeField] private HpBar _hpBar;

    private PlayerHealth _playerHealth;
    private ButtonForward _buttonForward;

    private void OnDisable()
    {
        _playerHealth.HealthChanged -= UpdateHpBar;
    }

    public void Construct(PlayerHealth playerHealth)
    {
        _playerHealth = playerHealth;
        _playerHealth.HealthChanged += UpdateHpBar;
        ConstructButtonForward();
    }

    private void ConstructButtonForward()
    {
        _buttonForward = GetComponentInChildren<ButtonForward>();
        _buttonForward.OnClick += OnClick;
    }

    private void OnClick() =>
        _playerHealth.GetComponent<PlayerMove>().PlayMove();

    private void UpdateHpBar()
    {
        _hpBar.SetValue(_playerHealth.Current, _playerHealth.Max);
    }
}