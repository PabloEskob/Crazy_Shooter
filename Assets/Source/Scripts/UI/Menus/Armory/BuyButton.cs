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
    [SerializeField] private Image _currencyIcon;

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

        ChangeButtonView(_weapon.GetFrameUpgrade().Level);
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
        ChangePrice();

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

        ChangePrice();
        DisplayPriceText();
        ChangeButtonView(_weapon.GetFrameUpgrade().Level);
    }

    private void ChangePrice()
    {
        if (_weapon.IsBought())
            SetPrice(_weapon.GetFrameUpgrade().Price);
        else
            SetPrice(_weapon.WeaponPrice);
    }

    private void OnUpgradeChoosed(UpgradeType upgrade)
    {
        SetUpgrade(upgrade);

        switch (_currentUpgrade)
        {
            case FrameUpgrade:
                SetPrice(_weapon.GetFrameUpgrade().Price);
                DisplayPriceText();
                ChangeButtonView(_weapon.GetFrameUpgrade().Level);
                break;
            case MuzzleUpgrade:
                SetPrice(_weapon.GetMuzzleUpgrade().Price);
                DisplayPriceText();
                ChangeButtonView(_weapon.GetMuzzleUpgrade().Level);
                break;
            case ScopeUpgrade:
                SetPrice(_weapon.GetScopeUpgrade().Price);
                DisplayPriceText();
                ChangeButtonView(_weapon.GetScopeUpgrade().Level);
                break;
            case BulletsUpgrade:
                SetPrice(_weapon.GetBulletsUpgrade().Price);
                DisplayPriceText();
                ChangeButtonView(_weapon.GetBulletsUpgrade().Level);
                break;
            case MagazineUpgrade:
                SetPrice(_weapon.GetMagazineUpgrade().Price);
                DisplayPriceText();
                ChangeButtonView(_weapon.GetMagazineUpgrade().Level);
                break;
        }

        //DisplayPriceText();

    }

    public void SetUpgrade(UpgradeType upgrade) => _currentUpgrade = upgrade;

    private void DisplayPriceText() =>
        _priceText.text = CurrentPrice == 0 ? FreeText : CurrentPrice.ToString();

    private void SetPrice(int price) =>
        CurrentPrice = price;

    public void ChangeButtonText(string text) =>
        _buttonText.text = text;


    public void ChangeButtonView(int level)
    {
        if (level != _weapon.MaxUpgradeLevel)
        {
            _button.interactable = true;
            _priceText.gameObject.SetActive(true);
            _currencyIcon.gameObject.SetActive(true);
        }
        else
        {
            _button.interactable = false;
            _priceText.gameObject.SetActive(false);
            _currencyIcon.gameObject.SetActive(false);
            ChangeButtonText("MAX");
        }
    }
}