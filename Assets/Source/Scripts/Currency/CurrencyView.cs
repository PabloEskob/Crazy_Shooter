using TMPro;
using UnityEngine;

public class CurrencyView : MonoBehaviour
{
    [SerializeField] private SoftCurrencyHolder _softCurrencyHolder;
    [SerializeField] private TMP_Text _currencyValueText;

    private void OnEnable()
    {
        _softCurrencyHolder.BalanceChanged += OnSoftBalanceChanged;
    }

    private void OnDisable()
    {
        _softCurrencyHolder.BalanceChanged -= OnSoftBalanceChanged;
    }

    private void OnSoftBalanceChanged(int balance)
    {
        _currencyValueText.text = balance.ToString();
    }
}
