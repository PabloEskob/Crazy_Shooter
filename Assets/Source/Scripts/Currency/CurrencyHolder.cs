using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CurrencyHolder : MonoBehaviour
{
    private Currency _currency;

    public event UnityAction<int> BalanceChanged;

    protected abstract Currency InitCurrency();

    public int Value => _currency.Value;
    public bool HasMoney => _currency.Value > 0;

    private void OnEnable()
    {
        _currency = InitCurrency();
        _currency.Changed += OnBalanceChanged;
    }

    private void OnDisable() => 
        _currency.Changed -= OnBalanceChanged;

    public void AddSoft(int value) => 
        _currency.Add(value);

    public void AddReward(int value) => 
        _currency.Add(value);

    public void Spend(int value) => 
        _currency.Spend(value);

    private void OnBalanceChanged() => 
        BalanceChanged?.Invoke(_currency.Value);

    public bool CheckSolvency(int price) =>
        Value >= price;
}
