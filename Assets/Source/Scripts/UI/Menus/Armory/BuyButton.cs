using System;
using System.Collections.Generic;
using InfimaGames.LowPolyShooterPack;
using Source.Infrastructure;
using Source.Scripts.Data;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
using Source.Scripts.Ui;
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
    public UpgradeType CurrentUpgrade { get; private set; }
    private UpgradeType _defaultUpgrade;
    private IStorage _storage;

    private const string FreeText = "Free";
    private const string MaxUpgradeText = "MAX";

    public int CurrentPrice { get; private set; }
    public Text PriceText => _priceText;
    public Button Button => _button;

    public event Action<int> UpgradeLevelSetted;

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
        OnUpgradeChoosed(_defaultUpgrade);
        ChangePriceView();
        ChangeButtonText(_weapon, 1);

        _storage = AllServices.Container.Single<IStorage>();

        if (_storage != null)
        {
            _storage.SetString(_weapon.GetName(), _weapon.GetData().ToJson());
            _storage.Save();
        }
    }

    private void OnUpgraded() =>
        OnUpgradeChoosed(CurrentUpgrade);

    private void OnWeaponSet(Weapon weapon)
    {
        _weapon.Bought -= OnWeaponBought;
        _weapon = weapon;
        _weapon.Bought += OnWeaponBought;
        OnUpgradeChoosed(_defaultUpgrade);
        ChangePriceView();
        DisplayPriceText();
        ChangeButtonView(_weapon.GetFrameUpgrade().Level);
    }

    private void ChangePriceView()
    {
        if (_weapon.IsBought())
            SetPrice(_weapon.GetFrameUpgrade().Price);
        else
            SetPrice(_weapon.WeaponPrice);
    }

    private void OnUpgradeChoosed(UpgradeType upgrade)
    {
        SetUpgrade(upgrade);

        switch (CurrentUpgrade)
        {
            case FrameUpgrade:
                SetPrice(_weapon.GetFrameUpgrade().Price);
                ChangeButtonView(_weapon.GetFrameUpgrade().Level);
                UpgradeLevelSetted?.Invoke(_weapon.GetFrameUpgrade().Level);
                break;
            case MuzzleUpgrade:
                SetPrice(_weapon.GetMuzzleUpgrade().Price);
                ChangeButtonView(_weapon.GetMuzzleUpgrade().Level);
                UpgradeLevelSetted?.Invoke(_weapon.GetMuzzleUpgrade().Level);
                break;
            case ScopeUpgrade:
                SetPrice(_weapon.GetScopeUpgrade().Price);
                ChangeButtonView(_weapon.GetScopeUpgrade().Level);
                UpgradeLevelSetted?.Invoke(_weapon.GetScopeUpgrade().Level);
                break;
            case BulletsUpgrade:
                SetPrice(_weapon.GetBulletsUpgrade().Price);
                ChangeButtonView(_weapon.GetBulletsUpgrade().Level);
                UpgradeLevelSetted?.Invoke(_weapon.GetBulletsUpgrade().Level);
                break;
            case MagazineUpgrade:
                SetPrice(_weapon.GetMagazineUpgrade().Price);
                ChangeButtonView(_weapon.GetMagazineUpgrade().Level);
                UpgradeLevelSetted?.Invoke(_weapon.GetMagazineUpgrade().Level);
                break;
        }

        DisplayPriceText();
    }

    public void SetUpgrade(UpgradeType upgrade) => CurrentUpgrade = upgrade;

    private void DisplayPriceText() =>
        _priceText.text = CurrentPrice == 0 ? FreeText : CurrentPrice.ToString();

    private void SetPrice(int price) =>
        CurrentPrice = price;

    public void ChangeButtonText(Weapon weapon, int level)
    {
        _buttonText.text = weapon.IsBought() ? _upgradePanel.UpgradeText : _upgradePanel.BuyText;

        if (level == weapon.MaxUpgradeLevel)
            _buttonText.text = MaxUpgradeText;
    }


    public void ChangeButtonView(int level)
    {
        if (level != _weapon.MaxUpgradeLevel)
        {
            _button.interactable = true;
            _priceText.gameObject.SetActive(true);
            _currencyIcon.gameObject.SetActive(true);
            ChangeButtonText(_weapon, level);
        }
        else
        {
            _button.interactable = false;
            _priceText.gameObject.SetActive(false);
            _currencyIcon.gameObject.SetActive(false);
            ChangeButtonText(_weapon, level);
        }
    }
}