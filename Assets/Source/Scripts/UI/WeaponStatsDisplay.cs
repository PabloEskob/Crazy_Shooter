using InfimaGames.LowPolyShooterPack;
using Source.Scripts.StaticData;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Source.Scripts.Ui
{
    public class WeaponStatsDisplay : MonoBehaviour
    {
        [SerializeField] private WeaponPlatesView _platesView;
        [SerializeField] private List<UpgradeType> _upgradeTypes;

        private Weapon _weapon;

        [Header("Current values")]
        [SerializeField] private TMP_Text _damageValue;
        [SerializeField] private TMP_Text _fireRateValue;
        [SerializeField] private TMP_Text _reloadValue;
        [SerializeField] private TMP_Text _magazineSizeValue;

        [Header("Upgrade values")]
        [SerializeField] private TMP_Text _upgradeDamageValue;
        [SerializeField] private TMP_Text _upgradeFireRateValue;
        [SerializeField] private TMP_Text _upgradeReloadValue;
        [SerializeField] private TMP_Text _upgradeMagazineSizeValue;

        private void OnEnable()
        {
            _weapon = _platesView.CurrentWeapon;
            _platesView.WeaponSelected += OnWeaponSelected;

            foreach (var upgrade in _upgradeTypes)
                upgrade.UpgradeChoosed += OnUpgradeChoosed;
        }

        private void OnDisable()
        {
            _platesView.WeaponSelected -= OnWeaponSelected;

            foreach (var upgrade in _upgradeTypes)
                upgrade.UpgradeChoosed -= OnUpgradeChoosed;
        }

        private void OnUpgradeChoosed(UpgradeType upgrade)
        {
            DisplayUpgradeValues(upgrade);
        }

        private void OnWeaponSelected(Weapon weapon)
        {
            SetWeapon(weapon);
            DisplayCurrentStats();
            DisplayUpgradeValues();
        }

        private void DisplayCurrentStats()
        {
            _damageValue.text = _weapon.Damage.ToString();
            _fireRateValue.text = _weapon.FireRate.ToString();
            _reloadValue.text = _weapon.ReloadSpeed.ToString();
            _magazineSizeValue.text = _weapon.MagazineSize.ToString();
        }

        private void DisplayUpgradeValues()
        {
            _upgradeDamageValue.text = $" +{_weapon.UpgradeConfig.GunFrameUpgrades[0].Damage}";
            _upgradeFireRateValue.text = $" +{_weapon.UpgradeConfig.GunFrameUpgrades[0].FireRate}";
            _upgradeReloadValue.text = $" +{_weapon.UpgradeConfig.GunFrameUpgrades[0].Reload}";
            _upgradeMagazineSizeValue.text = $" +{_weapon.UpgradeConfig.GunFrameUpgrades[0].MagazineSize}";
        }

        private void DisplayUpgradeValues(UpgradeType upgrade)
        {
            switch (upgrade)
            {
                case FrameUpgrade:
                    _upgradeDamageValue.text = $" +{_weapon.UpgradeConfig.GunFrameUpgrades[0].Damage}";
                    _upgradeFireRateValue.text = $" +{_weapon.UpgradeConfig.GunFrameUpgrades[0].FireRate}";
                    _upgradeReloadValue.text = $" +{_weapon.UpgradeConfig.GunFrameUpgrades[0].Reload}";
                    _upgradeMagazineSizeValue.text = $" +{_weapon.UpgradeConfig.GunFrameUpgrades[0].MagazineSize}";
                    break;
                case MuzzleUpgrade:
                    _upgradeDamageValue.text = $" +{_weapon.UpgradeConfig.MuzzleUpgrades[0].Damage}";
                    _upgradeFireRateValue.text = $" +{_weapon.UpgradeConfig.MuzzleUpgrades[0].FireRate}";
                    _upgradeReloadValue.text = $" +{_weapon.UpgradeConfig.MuzzleUpgrades[0].Reload}";
                    _upgradeMagazineSizeValue.text = $" +{_weapon.UpgradeConfig.MuzzleUpgrades[0].MagazineSize}";
                    break;
                case ScopeUpgrade:
                    _upgradeDamageValue.text = $" +{_weapon.UpgradeConfig.ScopeUpgrades[0].Damage}";
                    _upgradeFireRateValue.text = $" +{_weapon.UpgradeConfig.ScopeUpgrades[0].FireRate}";
                    _upgradeReloadValue.text = $" +{_weapon.UpgradeConfig.ScopeUpgrades[0].Reload}";
                    _upgradeMagazineSizeValue.text = $" +{_weapon.UpgradeConfig.ScopeUpgrades[0].MagazineSize}";
                    break;
                case BulletsUpgrade:
                    _upgradeDamageValue.text = $" +{_weapon.UpgradeConfig.BulletUpgrades[0].Damage}";
                    _upgradeFireRateValue.text = $" +{_weapon.UpgradeConfig.BulletUpgrades[0].FireRate}";
                    _upgradeReloadValue.text = $" +{_weapon.UpgradeConfig.BulletUpgrades[0].Reload}";
                    _upgradeMagazineSizeValue.text = $" +{_weapon.UpgradeConfig.BulletUpgrades[0].MagazineSize}";
                    break;
                case MagazineUpgrade:
                    _upgradeDamageValue.text = $" +{_weapon.UpgradeConfig.MagazineUpgrades[0].Damage}";
                    _upgradeFireRateValue.text = $" +{_weapon.UpgradeConfig.MagazineUpgrades[0].FireRate}";
                    _upgradeReloadValue.text = $" +{_weapon.UpgradeConfig.MagazineUpgrades[0].Reload}";
                    _upgradeMagazineSizeValue.text = $" +{_weapon.UpgradeConfig.MagazineUpgrades[0].MagazineSize}";
                    break;
            }
        }

        public void SetWeapon(Weapon weapon) =>
            _weapon = weapon;


    }
}
