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
        [SerializeField] private UpgradePanel _upgradePanel;

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

        private UpgradeType _currentUpgrade;
        private float _damage;
        private float _fireRate;
        private float _reload;
        private float _magazineSize;

        public event Action<float, float, float, float> ValuesSet;

        private void OnEnable()
        {
            _upgradePanel.Upgraded += OnUpgraded;
            _weapon = _platesView.CurrentWeapon;
            _platesView.WeaponSelected += OnWeaponSelected;

            foreach (var upgrade in _upgradeTypes)
                upgrade.UpgradeChoosed += OnUpgradeChoosed;

            DisplayCurrentStats();
            DisplayUpgradeValues();
        }

        private void OnDisable()
        {
            _upgradePanel.Upgraded -= OnUpgraded;
            _platesView.WeaponSelected -= OnWeaponSelected;

            foreach (var upgrade in _upgradeTypes)
                upgrade.UpgradeChoosed -= OnUpgradeChoosed;
        }

        private void OnUpgraded()
        {
            DisplayCurrentStats();
            DisplayUpgradeValues(_currentUpgrade);
        }

        private void OnUpgradeChoosed(UpgradeType upgrade) => 
            DisplayUpgradeValues(upgrade);

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
            var frameUpgrade = _weapon.UpgradeConfig.GunFrameUpgrades[0];
            _upgradeDamageValue.text = $" +{frameUpgrade.Damage}";
            _upgradeFireRateValue.text = $" +{frameUpgrade.FireRate}";
            _upgradeReloadValue.text = $" +{frameUpgrade.Reload}";
            _upgradeMagazineSizeValue.text = $" +{frameUpgrade.MagazineSize}";
            SetValues(frameUpgrade.Damage, frameUpgrade.FireRate, frameUpgrade.Reload, frameUpgrade.MagazineSize);
        }

        private void DisplayUpgradeValues(UpgradeType upgrade)
        {
            _currentUpgrade = upgrade;

            switch (upgrade)
            {
                case FrameUpgrade:
                    var frameUpgrade = _weapon.GetFrameUpgrade();
                    _upgradeDamageValue.text = $" +{frameUpgrade.Damage}";
                    _upgradeFireRateValue.text = $" +{frameUpgrade.FireRate}";
                    _upgradeReloadValue.text = $" +{frameUpgrade.Reload}";
                    _upgradeMagazineSizeValue.text = $" +{frameUpgrade.MagazineSize}";
                    SetValues(frameUpgrade.Damage, frameUpgrade.FireRate, frameUpgrade.Reload, frameUpgrade.MagazineSize);
                    break;
                case MuzzleUpgrade:
                    var muzzleUpgrade = _weapon.GetMuzzleUpgrade();
                    _upgradeDamageValue.text = $" +{muzzleUpgrade.Damage}";
                    _upgradeFireRateValue.text = $" +{muzzleUpgrade.FireRate}";
                    _upgradeReloadValue.text = $" +{muzzleUpgrade.Reload}";
                    _upgradeMagazineSizeValue.text = $" +{muzzleUpgrade.MagazineSize}";
                    SetValues(muzzleUpgrade.Damage, muzzleUpgrade.FireRate, muzzleUpgrade.Reload, muzzleUpgrade.MagazineSize);
                    break;
                case ScopeUpgrade:
                    var scopeUpgrade = _weapon.GetScopeUpgrade();
                    _upgradeDamageValue.text = $" +{scopeUpgrade.Damage}";
                    _upgradeFireRateValue.text = $" +{scopeUpgrade.FireRate}";
                    _upgradeReloadValue.text = $" +{scopeUpgrade.Reload}";
                    _upgradeMagazineSizeValue.text = $" +{scopeUpgrade.MagazineSize}";
                    SetValues(scopeUpgrade.Damage, scopeUpgrade.FireRate, scopeUpgrade.Reload, scopeUpgrade.MagazineSize);
                    break;
                case BulletsUpgrade:
                    var bulletUpgrade = _weapon.GetBulletsUpgrade();
                    _upgradeDamageValue.text = $" +{bulletUpgrade.Damage}";
                    _upgradeFireRateValue.text = $" +{bulletUpgrade.FireRate}";
                    _upgradeReloadValue.text = $" +{bulletUpgrade.Reload}";
                    _upgradeMagazineSizeValue.text = $" +{bulletUpgrade.MagazineSize}";
                    SetValues(bulletUpgrade.Damage, bulletUpgrade.FireRate, bulletUpgrade.Reload, bulletUpgrade.MagazineSize);
                    break;
                case MagazineUpgrade:
                    var magazineUpgrade = _weapon.GetMagazineUpgrade();
                    _upgradeDamageValue.text = $" +{magazineUpgrade.Damage}";
                    _upgradeFireRateValue.text = $" +{magazineUpgrade.FireRate}";
                    _upgradeReloadValue.text = $" +{magazineUpgrade.Reload}";
                    _upgradeMagazineSizeValue.text = $" +{magazineUpgrade.MagazineSize}";
                    SetValues(magazineUpgrade.Damage, magazineUpgrade.FireRate, magazineUpgrade.Reload, magazineUpgrade.MagazineSize);
                    break;
            }
        }

        private void SetValues(float damage, float fireRate, float reload, float magazineSize)
        {
            _damage = damage;
            _fireRate = fireRate;
            _reload = reload;
            _magazineSize = magazineSize;

            ValuesSet?.Invoke(_damage, _fireRate, _reload, _magazineSize);
        }

        public void SetWeapon(Weapon weapon) =>
            _weapon = weapon;


    }
}
