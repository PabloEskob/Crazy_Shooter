using System;
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

    private Weapon Weapon => _upgradeHandler.GetWeapon();
    public UpgradeType CurrentUpgrade { get; private set; }
    private UpgradeType _defaultUpgrade;

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
    }

    private void OnDisable()
    {
        _upgradeHandler.Bought += OnBought;
        _upgradeHandler.Upgraded -= OnUpgraded;
        _upgradeHandler.WeaponSetted -= OnWeaponSet;
        _upgradeHandler.UpgradeSelected -= OnUpgradeSelected;
    }

    private void Start()
    {
        ChangePriceView();
        DisplayPriceText();
        ChangeButtonView(_upgradeHandler.GetWeaponUpgradeData().Level);
    }

    private void OnUpgradeSelected(UpgradeType type)
    {
        ChangePriceView();
        DisplayPriceText();
        ChangeButtonView(_upgradeHandler.GetWeaponUpgradeData().Level);
    }

    private void OnUpgraded()
    {
        ChangePriceView();
        DisplayPriceText();
        ChangeButtonView(_upgradeHandler.GetWeaponUpgradeData().Level);
        Debug.Log($"Level {_upgradeHandler.GetWeaponUpgradeData().Level}");
    }

    private void OnBought()
    {
        ChangePriceView();
        DisplayPriceText();
        ChangeButtonText(Weapon, _upgradeHandler.GetWeaponUpgradeData().Level);
    }

    private void OnWeaponSet(Weapon weapon)
    {
        ChangePriceView();
        DisplayPriceText();
        ChangeButtonView(_upgradeHandler.GetWeaponUpgradeData().Level);
    }

    private void ChangePriceView()
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
        if (level == weapon.MaxUpgradeLevel)
            _buttonText.text = MaxUpgradeText;

        if (weapon.IsBought())
            _buttonText.text = "Уличшить";
        else
            _buttonText.text = "Купить";
    }


    public void ChangeButtonView(int level)
    {
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