using Source.Infrastructure;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.Events;

public abstract class CurrencyHolder : MonoBehaviour
{
    private IStorage _storage;
    private Currency _currency;

    public event UnityAction<int> BalanceChanged;

    protected abstract Currency InitCurrency();

    public int Value => _currency.Value;
    public bool HasMoney => _currency.Value > 0;

    private void OnEnable()
    {
        _storage = AllServices.Container.Single<IStorage>();
        _currency = InitCurrency();
        _currency.Changed += OnBalanceChanged;
    }

    private void OnDisable() =>
        _currency.Changed -= OnBalanceChanged;

    private void Start() => 
        _currency.Add(_storage.GetSoft());

    public void AddSoft(int value) =>
        _currency.Add(value);

    public void AddReward(int value) =>
        _currency.Add(value);

    public void Spend(int value) =>
        _currency.Spend(value);

    private void OnBalanceChanged()
    {
        BalanceChanged?.Invoke(_currency.Value);
        _storage.SetSoft(_currency.Value);
        _storage.Save();
    }

    public bool CheckSolvency(int price) =>
        Value >= price;
}
