using System;
using Assets.Source.Scripts.Localization;
using Assets.Source.Scripts.UI.Menus.Armory;
using InfimaGames.LowPolyShooterPack;
using Source.Scripts.Ui;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    [SerializeField] private UpgradeHandler _upgradeHandler;
    [SerializeField] private Button _button;
    [SerializeField] private Text _priceText;
    [SerializeField] private Text _buttonText;
    [SerializeField] private Image _currencyIcon;
    [SerializeField] private Text _buyText;
    [SerializeField] private Text _upgradeText;

    private Weapon Weapon => _upgradeHandler.GetWeapon();
    public UpgradeType CurrentUpgrade { get; private set; }

    private const string MaxUpgradeText = "MAX";

    public int CurrentPrice { get; private set; }
    public Text PriceText => _priceText;
    public Button Button => _button;

    private void OnEnable()
    {
        _upgradeHandler.Bought += OnBought;
        _upgradeHandler.Upgraded += OnUpgraded;
        _upgradeHandler.WeaponSetted += OnWeaponSet;
        _upgradeHandler.UpgradeSelected += OnUpgradeSelected;
        ChangeButtonView(_upgradeHandler.GetWeaponUpgradeData().Level);
    }

    private void OnDisable()
    {
        _upgradeHandler.Bought -= OnBought;
        _upgradeHandler.Upgraded -= OnUpgraded;
        _upgradeHandler.WeaponSetted -= OnWeaponSet;
        _upgradeHandler.UpgradeSelected -= OnUpgradeSelected;
    }

    private void Start() => 
        ChangeButtonView(_upgradeHandler.GetWeaponUpgradeData().Level);

    private void OnUpgradeSelected(UpgradeType type) => 
        ChangeButtonView(_upgradeHandler.GetWeaponUpgradeData().Level);

    private void OnUpgraded() => 
        ChangeButtonView(_upgradeHandler.GetWeaponUpgradeData().Level);

    private void OnBought()
    {
        SetPriceView();
        DisplayPriceText();
        ChangeButtonText(Weapon, _upgradeHandler.GetWeaponUpgradeData().Level);
    }

    private void OnWeaponSet(Weapon weapon) => 
        ChangeButtonView(_upgradeHandler.GetWeaponUpgradeData().Level);

    private void SetPriceView()
    {
        if (Weapon.IsBought())
            SetPrice(_upgradeHandler.GetWeaponUpgradeData().Price);
        else
            SetPrice(Weapon.WeaponPrice);
    }


    private void DisplayPriceText() =>
        _priceText.text = CurrentPrice.ToString();

    private void SetPrice(int price) =>
        CurrentPrice = price;

    public void ChangeButtonText(Weapon weapon, int level)
    {
        if (weapon.IsBought())
            _buttonText.text = _upgradeText.text;
        else
            _buttonText.text = _buyText.text;

        if (level == weapon.MaxUpgradeLevel)
            _buttonText.text = MaxUpgradeText;
    }


    public void ChangeButtonView(int level)
    {
        SetPriceView();
        DisplayPriceText();

        if (level != Weapon.MaxUpgradeLevel)
        {
            _button.interactable = true;
            _priceText.gameObject.SetActive(true);
            _currencyIcon.gameObject.SetActive(true);
            ChangeButtonText(Weapon, level);
        }
        else
        {
            _button.interactable = false;
            _priceText.gameObject.SetActive(false);
            _currencyIcon.gameObject.SetActive(false);
            ChangeButtonText(Weapon, level);
        }
    }
}