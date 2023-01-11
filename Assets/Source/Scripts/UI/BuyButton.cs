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
    [SerializeField] private TMP_Text _buttonText;

    private Weapon _weapon;
    private int _currentPrice;
    private UpgradeType _currentUpgrade;
    private UpgradeType _defaultUpgrade;


    public TMP_Text PriceText => _priceText;
    public Button Button => _button;

    private void Awake()
    {
        _defaultUpgrade = _upgradeTypes[0];
    }

    private void OnEnable()
    {
        _weapon = _upgradePanel.CurrentWeapon;
        _upgradePanel.WeaponSet += OnWeaponSet;
        _upgradePanel.Upgraded += OnUpgraded;

        foreach (var upgrade in _upgradeTypes)
            upgrade.UpgradeChoosed += OnUpgradeChoosed;

        _button.interactable = _weapon.GetFrameUpgrade().Level != _weapon.MaxUpgradeLevel;

        OnUpgradeChoosed(_defaultUpgrade);
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

        _button.interactable = _weapon.GetFrameUpgrade().Level != _weapon.MaxUpgradeLevel;

    }

    private void OnUpgradeChoosed(UpgradeType upgrade)
    {
        _currentUpgrade = upgrade;

        switch (upgrade)
        {
            case FrameUpgrade:
                SetPrice(_weapon.GetFrameUpgrade().Price);

                _button.interactable = _weapon.GetFrameUpgrade().Level != _weapon.MaxUpgradeLevel;

                break;
            case MuzzleUpgrade:
                SetPrice(_weapon.GetMuzzleUpgrade().Price);

                _button.interactable = _weapon.GetMuzzleUpgrade().Level != _weapon.MaxUpgradeLevel;

                break;
            case ScopeUpgrade:
                SetPrice(_weapon.GetScopeUpgrade().Price);

                _button.interactable = _weapon.GetScopeUpgrade().Level != _weapon.MaxUpgradeLevel;

                break;
            case BulletsUpgrade:
                SetPrice(_weapon.GetBulletsUpgrade().Price);

                _button.interactable = _weapon.GetBulletsUpgrade().Level != _weapon.MaxUpgradeLevel;

                break;
            case MagazineUpgrade:
                SetPrice(_weapon.GetMagazineUpgrade().Price);

                _button.interactable = _weapon.GetMagazineUpgrade().Level != _weapon.MaxUpgradeLevel;

                break;
        }

        DisplayPriceText();
    }

    private void DisplayPriceText() =>
        _priceText.text = _currentPrice.ToString();

    private void SetPrice(int price) =>
        _currentPrice = price;

    public void ChangeButtonText(string text)
    {
        _buttonText.text= text;
    }

}