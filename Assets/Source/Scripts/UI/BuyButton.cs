using System.Collections.Generic;
using InfimaGames.LowPolyShooterPack;
using Source.Scripts.Ui;
using TMPro;
using UnityEngine;

public class BuyButton : MonoBehaviour
{
    [SerializeField] private List<UpgradeType> _upgradeTypes;
    [SerializeField] private UpgradePanel _upgradePanel;
    [SerializeField] private TMP_Text _price;

    private Weapon _weapon;

    private void OnEnable()
    {
        _weapon = _upgradePanel.CurrentWeapon;
        _upgradePanel.WeaponSet += OnWeaponSet;

        foreach (var upgrade in _upgradeTypes)
            upgrade.UpgradeChoosed += OnUpgradeChoosed;
    }

    private void OnDisable()
    {
        _upgradePanel.WeaponSet -= OnWeaponSet;

        foreach (var upgrade in _upgradeTypes)
            upgrade.UpgradeChoosed -= OnUpgradeChoosed;
    }

    private void OnWeaponSet(Weapon weapon)
    {
        _weapon = weapon;
        SetPriceText(_weapon.UpgradeConfig.GunFrameUpgrades[0].Price);
    }

    private void OnUpgradeChoosed(UpgradeType upgrade)
    {
        switch (upgrade)
        {
            case FrameUpgrade:
                SetPriceText(_weapon.UpgradeConfig.GunFrameUpgrades[0].Price);
                break;
            case MuzzleUpgrade:
                SetPriceText(_weapon.UpgradeConfig.MuzzleUpgrades[0].Price);
                break;
            case ScopeUpgrade:
                SetPriceText(_weapon.UpgradeConfig.ScopeUpgrades[0].Price);
                break;
            case BulletsUpgrade:
                SetPriceText(_weapon.UpgradeConfig.BulletUpgrades[0].Price);
                break;
            case MagazineUpgrade:
                SetPriceText(_weapon.UpgradeConfig.MagazineUpgrades[0].Price);
                break;
        }
    }


    private void SetPriceText(int price) => 
        _price.text = price.ToString();
}