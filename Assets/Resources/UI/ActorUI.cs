using UnityEngine;

public class ActorUI : MonoBehaviour
{
    [SerializeField] private HpBar _hpBar;

    private PlayerHealth _playerHealth;

    private void OnDisable() =>
        _playerHealth.HealthChanged -= UpdateHpBar;

    public void Construct(PlayerHealth playerHealth)
    {
        _playerHealth = playerHealth;
        _playerHealth.HealthChanged += UpdateHpBar;
    }

    private void UpdateHpBar()
    {
        _hpBar.SetValue(_playerHealth.Current, _playerHealth.Max);
    }
}