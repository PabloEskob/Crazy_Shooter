using System;
using Agava.YandexGames;
using Assets.Source.Scripts.Advertisement;
using Assets.Source.Scripts.UI.Menus.Armory;
using Assets.Source.Scripts.UI.Menus.Rewards;
using InfimaGames.LowPolyShooterPack;
using Source.Scripts.Ui;
using UnityEngine;

public class UpgradeViewPanel : MonoBehaviour
{
    [SerializeField] private UpgradeHandler _upgradeHandler;
    [SerializeField] private BuyButton _buyButton;
    [SerializeField] private AdvertisementButton _upgradeButtonAd;
    [SerializeField] private WeaponStatsDisplay _statsDisplay;
    [SerializeField] private SoftCurrencyHolder _softCurrencyHolder;
    [SerializeField] private AdvertisementHandler _advertisingHandler;


    private float _additionalDamage;
    private float _additionalFireRate;
    private float _additionalReloadSpeed;
    private float _additionalMagazinSize;

    public Weapon CurrentWeapon => _upgradeHandler.GetWeapon();
    public int CurrentUpgradeLevel { get; private set; }

    public event Action<Weapon> WeaponSet;
    public event Action Upgraded;

    private void OnEnable()
    {
        _statsDisplay.ValuesSet += OnValuesSet;
        _buyButton.Button.onClick.AddListener(OnBuyButtonClick);
        _upgradeButtonAd.ButtonClicked += OnAdButtonClick;
    }


    private void OnDisable()
    {
        _statsDisplay.ValuesSet -= OnValuesSet;
        _buyButton.Button.onClick.RemoveListener(OnBuyButtonClick);
        _upgradeButtonAd.ButtonClicked -= OnAdButtonClick;
    }

    private void OnValuesSet(float damage, float fireRate, float reloadSpeed, float magazineSize) =>
        UpdateUpgradeValues(damage, fireRate, reloadSpeed, magazineSize);


    private void UpdateUpgradeValues(float damage, float fireRate, float reloadSpeed, float magazineSize)
    {
        _additionalDamage = damage;
        _additionalFireRate = fireRate;
        _additionalReloadSpeed = reloadSpeed;
        _additionalMagazinSize = magazineSize;
    }

    private void OnBuyButtonClick()
    {
        if (_softCurrencyHolder.CheckSolvency(_buyButton.CurrentPrice))
        {
            if (CurrentWeapon.IsBought())
            {
                _softCurrencyHolder.Spend(_buyButton.CurrentPrice);
                _upgradeHandler.Upgrade(_additionalDamage, _additionalFireRate, _additionalReloadSpeed, _additionalMagazinSize);
                Upgraded?.Invoke();
            }
            else
            {
                _softCurrencyHolder.Spend(_buyButton.CurrentPrice);
                _upgradeHandler.Buy();
            }
        }
    }

    private void OnAdClosed()
    {
        _upgradeHandler.Upgrade(_additionalDamage, _additionalFireRate, _additionalReloadSpeed, _additionalMagazinSize);
        Upgraded?.Invoke();
    }

    private void OnAdButtonClick()
    {
        if (CurrentWeapon.MaxUpgradeLevel != CurrentUpgradeLevel && CurrentWeapon.IsBought())
            ShowAd();
    }

    private void ShowAd()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
            VideoAd.Show(OnAdStart, null, OnAdClosed);
#endif

#if UNITY_EDITOR
        OnAdClosed();
#endif
    }

    private void OnAdStart()
    {

    }
}