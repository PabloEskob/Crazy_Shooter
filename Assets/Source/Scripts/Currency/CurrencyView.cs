using UnityEngine;
using UnityEngine.UI;

public class CurrencyView : MonoBehaviour
{
    [SerializeField] private SoftCurrencyHolder _softCurrencyHolder;
    [SerializeField] private Text _currencyValueText;

    private void OnEnable()
    {
        _softCurrencyHolder.BalanceChanged += OnSoftBalanceChanged;
        DisplayCurrency(_softCurrencyHolder.Value);
    }

    private void OnDisable() =>
        _softCurrencyHolder.BalanceChanged -= OnSoftBalanceChanged;

    private void OnSoftBalanceChanged(int balance) =>
        DisplayCurrency(balance);

    private void DisplayCurrency(int balance) => 
        _currencyValueText.text = balance.ToString();
}
