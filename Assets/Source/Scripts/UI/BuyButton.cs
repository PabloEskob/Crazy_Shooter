using System;
using System.Collections.Generic;
using InfimaGames.LowPolyShooterPack;
using Source.Scripts.Ui;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private List<UpgradeType> _upgradeTypes;
    [SerializeField] private UpgradePanel _upgradePanel;
    [SerializeField] private TMP_Text _priceText;

    private Weapon _weapon;
    private int _currentPrice;
    private UpgradeType _currentUpgrade;

    public Button Button => _button;

    private void OnEnable()
    {
        _weapon = _upgradePanel.CurrentWeapon;
        _upgradePanel.WeaponSet += OnWeaponSet;
        _upgradePanel.Upgraded += OnUpgraded;

        foreach (var upgrade in _upgradeTypes)
            upgrade.UpgradeChoosed += OnUpgradeChoosed;
    }

    private void OnDisable()
    {
        _upgradePanel.WeaponSet -= OnWeaponSet;
        _upgradePanel.Upgraded -= OnUpgraded;

        foreach (var upgrade in _upgradeTypes)
            upgrade.UpgradeChoosed -= OnUpgradeChoosed;
    }

    private void OnUpgraded()
    {
        OnUpgradeChoosed(_currentUpgrade);
    }

    private void OnWeaponSet(Weapon weapon)
    {
        _weapon = weapon;
        SetPrice(_weapon.GetFrameUpgrade().Price);
        DisplayPriceText();

    }

    private void OnUpgradeChoosed(UpgradeType upgrade)
    {
        _currentUpgrade = upgrade;

        switch (upgrade)
        {
            case FrameUpgrade:
                SetPrice(_weapon.GetFrameUpgrade().Price);
                break;
            case MuzzleUpgrade:
                SetPrice(_weapon.GetMuzzleUpgrade().Price);
                break;
            case ScopeUpgrade:
                SetPrice(_weapon.GetScopeUpgrade().Price);
                break;
            case BulletsUpgrade:
                SetPrice(_weapon.GetBulletsUpgrade().Price);
                break;
            case MagazineUpgrade:
                SetPrice(_weapon.GetMagazineUpgrade().Price);
                break;
        }

        DisplayPriceText();
    }

    private void DisplayPriceText() =>
        _priceText.text = _currentPrice.ToString();

    private void SetPrice(int price) =>
        _currentPrice = price;

}