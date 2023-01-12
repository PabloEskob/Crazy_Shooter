using System;
using System.Collections.Generic;
using InfimaGames.LowPolyShooterPack;
using Source.Scripts.Ui;
using TMPro;
using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] private List<UpgradeType> _upgradeTypeButtons;
    [SerializeField] private BuyButton _buyButton;
    [SerializeField] private WeaponStatsDisplay _statsDisplay;

    private Weapon _currentWeapon;
    private UpgradeType _defaultUpgradeType;
    private float _additionalDamage;
    private float _additionalFireRate;
    private float _additionalReloadSpeed;
    private float _additionalMagazinSize;

    public Weapon CurrentWeapon => _currentWeapon;

    public event Action<Weapon> WeaponSet;
    public event Action Upgraded;

    private void OnEnable()
    {
        _statsDisplay.ValuesSet += OnValuesSet;
        _buyButton.Button.onClick.AddListener(OnBuyButtonClick);

        _defaultUpgradeType = _upgradeTypeButtons[0];
        OnUpgradeChoosed(_defaultUpgradeType);

        foreach (UpgradeType button in _upgradeTypeButtons)
            button.UpgradeChoosed += OnUpgradeChoosed;

        WeaponSet += OnWeaponSet;
    }

    private void OnDisable()
    {
        _statsDisplay.ValuesSet -= OnValuesSet;
        _buyButton.Button.onClick.RemoveListener(OnBuyButtonClick);

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
        foreach (var button in _upgradeTypeButtons)
            button.SwitchButtonInteractivity(weapon.IsBought());

        if (!weapon.IsBought())
            _buyButton.ChangeButtonText("Buy");
        else
            _buyButton.ChangeButtonText("Upgrade");

        foreach (UpgradeType typeButton in _upgradeTypeButtons)
            typeButton.SetText();
    }

    private void OnBuyButtonClick()
    {
        if (_currentWeapon.IsBought())
        {
            _currentWeapon.Upgrade(_additionalDamage, _additionalFireRate, _additionalReloadSpeed, _additionalMagazinSize);
            _currentWeapon.UpdateStatsToData();
            Upgraded?.Invoke();
        }
        else
        {
            _currentWeapon.SetIsBought();
            OnWeaponSet(_currentWeapon);
        }

    }
}