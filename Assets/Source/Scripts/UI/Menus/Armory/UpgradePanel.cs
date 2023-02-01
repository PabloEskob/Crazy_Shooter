using System;
using System.Collections.Generic;
using Assets.Source.Scripts.UI.Menus.Armory;
using InfimaGames.LowPolyShooterPack;
using Source.Scripts.Ui;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] private List<UpgradeType> _upgradeTypeButtons;
    [SerializeField] private BuyButton _buyButton;
    [SerializeField] private UpgradeButtonAd _upgradeButtonAd;
    [SerializeField] private WeaponStatsDisplay _statsDisplay;
    [SerializeField] private SoftCurrencyHolder _softCurrencyHolder;
    [SerializeField] private Text _buyText;
    [SerializeField] private Text _upgradeText;

    private Weapon _currentWeapon;
    private UpgradeType _defaultUpgradeType;
    private float _additionalDamage;
    private float _additionalFireRate;
    private float _additionalReloadSpeed;
    private float _additionalMagazinSize;

    public string BuyText => _buyText.text;
    public string UpgradeText => _upgradeText.text;
    public Weapon CurrentWeapon => _currentWeapon;
    public int CurrentUpgradeLevel { get; private set; }

    public event Action<Weapon> WeaponSet;
    public event Action Upgraded;

    private void OnEnable()
    {
        _defaultUpgradeType = _upgradeTypeButtons[0];
        _statsDisplay.ValuesSet += OnValuesSet;
        _buyButton.Button.onClick.AddListener(OnBuyButtonClick);
        _buyButton.UpgradeLevelSetted += OnUpgradeLevelSetted;
        _upgradeButtonAd.Button.onClick.AddListener(OnAdButtonClick);
        _statsDisplay.SetUpgrade(_defaultUpgradeType);
        _buyButton.SetUpgrade(_defaultUpgradeType);
        OnUpgradeChoosed(_defaultUpgradeType);

        foreach (UpgradeType button in _upgradeTypeButtons)
            button.UpgradeChoosed += OnUpgradeChoosed;

        WeaponSet += OnWeaponSet;
    }


    private void OnDisable()
    {
        _statsDisplay.ValuesSet -= OnValuesSet;
        _buyButton.Button.onClick.RemoveListener(OnBuyButtonClick);
        _buyButton.UpgradeLevelSetted -= OnUpgradeLevelSetted;
        _upgradeButtonAd.Button.onClick.RemoveListener(OnAdButtonClick);

        foreach (UpgradeType button in _upgradeTypeButtons)
            button.UpgradeChoosed -= OnUpgradeChoosed;

        WeaponSet -= OnWeaponSet;
    }

    private void OnValuesSet(float damage, float fireRate, float reloadSpeed, float magazineSize) =>
        UpdateUpgradeValues(damage, fireRate, reloadSpeed, magazineSize);

    private void OnUpgradeChoosed(UpgradeType upgradeType)
    {
        foreach (UpgradeType typeButton in _upgradeTypeButtons)
        {
            typeButton.SwitchButtonState(false);
            typeButton.SetText();
        }

        upgradeType.SwitchButtonState(true);
        upgradeType.SetText();
    }

    private void UpdateUpgradeValues(float damage, float fireRate, float reloadSpeed, float magazineSize)
    {
        _additionalDamage = damage;
        _additionalFireRate = fireRate;
        _additionalReloadSpeed = reloadSpeed;
        _additionalMagazinSize = magazineSize;
    }

    public void SetWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
        WeaponSet?.Invoke(_currentWeapon);

        foreach (UpgradeType typeButton in _upgradeTypeButtons)
        {
            typeButton.SetWeapon(weapon);
            typeButton.SetText();
        }
    }

    private void OnWeaponSet(Weapon weapon)
    {
        OnUpgradeChoosed(_defaultUpgradeType);

        foreach (var button in _upgradeTypeButtons)
            button.SwitchButtonInteractivity(weapon.IsBought());

        foreach (UpgradeType typeButton in _upgradeTypeButtons)
            typeButton.SetText();
    }

    private void OnBuyButtonClick()
    {
        if (_softCurrencyHolder.CheckSolvency(_buyButton.CurrentPrice))
        {
            if (_currentWeapon.IsBought())
            {
                Upgraded?.Invoke();
                _softCurrencyHolder.Spend(_buyButton.CurrentPrice);
                _currentWeapon.Upgrade(_additionalDamage, _additionalFireRate, _additionalReloadSpeed, _additionalMagazinSize);
                _currentWeapon.UpdateStatsToData();
                Upgraded?.Invoke();
            }
            else
            {
                _softCurrencyHolder.Spend(_buyButton.CurrentPrice);
                _currentWeapon.SetIsBought();
                OnWeaponSet(_currentWeapon);
                _buyButton.SetUpgrade(_defaultUpgradeType);
            }
        }
    }

    private void OnUpgradeLevelSetted(int level)
    {
        SetUpgradeLevel(level);
    }

    private void SetUpgradeLevel(int level) => CurrentUpgradeLevel = level;

    private void OnAdClosed()
    {
        if (_currentWeapon.IsBought())
        {
            Upgraded?.Invoke();
            _currentWeapon.Upgrade(_additionalDamage, _additionalFireRate, _additionalReloadSpeed, _additionalMagazinSize);
            _currentWeapon.UpdateStatsToData();
            Upgraded?.Invoke();
        }
    }

    private void OnAdButtonClick()
    {
        if (CurrentWeapon.MaxUpgradeLevel != CurrentUpgradeLevel)
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

    public void UpdateTexts()
    {
        foreach (UpgradeType button in _upgradeTypeButtons)
            button.SetText();
    }
}