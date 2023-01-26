using System;
using System.Collections.Generic;
using InfimaGames.LowPolyShooterPack;
using Source.Infrastructure;
using Source.Scripts.Data;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
using Source.Scripts.Ui;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private List<UpgradeType> _upgradeTypes;
    [SerializeField] private UpgradePanel _upgradePanel;
    [SerializeField] private Text _priceText;
    [SerializeField] private Text _buttonText;
    [SerializeField] private SoftCurrencyHolder _softCurrencyHolder;

    private Weapon _weapon;
    private UpgradeType _currentUpgrade;
    private UpgradeType _defaultUpgrade;
    private IStorage _storage;

    private const string FreeText = "Free";

    public int CurrentPrice { get; private set; }
    public Text PriceText => _priceText;
    public Button Button => _button;

    private void Awake() =>
        _defaultUpgrade = _upgradeTypes[0];

    private void OnEnable()
    {
        _weapon = _upgradePanel.CurrentWeapon;
        _upgradePanel.WeaponSet += OnWeaponSet;
        _upgradePanel.Upgraded += OnUpgraded;
        _weapon.Bought += OnWeaponBought;

        foreach (var upgrade in _upgradeTypes)
            upgrade.UpgradeChoosed += OnUpgradeChoosed;

        ChengeButtonInteractable(_weapon.GetFrameUpgrade().Level);
        OnUpgradeChoosed(_defaultUpgrade);
    }

    private void OnDisable()
    {
        _upgradePanel.WeaponSet -= OnWeaponSet;
        _upgradePanel.Upgraded -= OnUpgraded;
        _weapon.Bought -= OnWeaponBought;

        foreach (var upgrade in _upgradeTypes)
            upgrade.UpgradeChoosed -= OnUpgradeChoosed;
    }

    private void OnWeaponBought()
    {
        SetPriceText();

        _storage = AllServices.Container.Single<IStorage>();

        if (_storage != null)
        {
            _storage.SetString(_weapon.GetName(), _weapon.GetData().ToJson());
            _storage.Save();
        }
    }

    private void OnUpgraded() =>
        OnUpgradeChoosed(_currentUpgrade);

    private void OnWeaponSet(Weapon weapon)
    {
        _weapon.Bought -= OnWeaponBought;
        _weapon = weapon;
        _weapon.Bought += OnWeaponBought;

        SetPriceText();
        DisplayPriceText();
        ChengeButtonInteractable(_weapon.GetFrameUpgrade().Level);
    }

    private void SetPriceText()
    {
        if (_weapon.IsBought())
            SetPrice(_weapon.GetFrameUpgrade().Price);
        else
            SetPrice(_weapon.WeaponPrice);
    }

    private void OnUpgradeChoosed(UpgradeType upgrade)
    {
        _currentUpgrade = upgrade;

        switch (upgrade)
        {
            case FrameUpgrade:
                SetPrice(_weapon.GetFrameUpgrade().Price);
                ChengeButtonInteractable(_weapon.GetFrameUpgrade().Level);
                break;
            case MuzzleUpgrade:
                SetPrice(_weapon.GetMuzzleUpgrade().Price);
                ChengeButtonInteractable(_weapon.GetMuzzleUpgrade().Level);
                break;
            case ScopeUpgrade:
                SetPrice(_weapon.GetScopeUpgrade().Price);
                ChengeButtonInteractable(_weapon.GetScopeUpgrade().Level);
                break;
            case BulletsUpgrade:
                SetPrice(_weapon.GetBulletsUpgrade().Price);
                ChengeButtonInteractable(_weapon.GetBulletsUpgrade().Level);
                break;
            case MagazineUpgrade:
                SetPrice(_weapon.GetMagazineUpgrade().Price);
                ChengeButtonInteractable(_weapon.GetMagazineUpgrade().Level);
                break;
        }

        DisplayPriceText();
    }

    private void DisplayPriceText() =>
        _priceText.text = CurrentPrice == 0 ? FreeText : CurrentPrice.ToString();

    private void SetPrice(int price) =>
        CurrentPrice = price;

    public void ChangeButtonText(string text) =>
        _buttonText.text = text;

    public void ChengeButtonInteractable(int level) =>
        _button.interactable = level != _weapon.MaxUpgradeLevel;

}