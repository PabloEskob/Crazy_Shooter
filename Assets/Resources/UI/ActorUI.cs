using UnityEngine;

public class ActorUI : MonoBehaviour
{
    [SerializeField] private HpBar _hpBar;

    private CarHealth _carHealth;

    private void OnDisable() =>
        _carHealth.HealthChanged -= UpdateHpBar;

    public void Construct(CarHealth carHealth)
    {
        _carHealth = carHealth;
        _carHealth.HealthChanged += UpdateHpBar;
    }

    private void UpdateHpBar()
    {
        _hpBar.SetValue(_carHealth.Current, _carHealth.Max);
    }
}