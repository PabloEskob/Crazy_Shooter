using InfimaGames.LowPolyShooterPack;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

        [Header("Upgrade Progress Bars")]
        [Space(3)]
        [SerializeField] private float _barLenghtMaxValue;
        
        [Header("Damage")]
        [SerializeField] Image _currentDamageValue;
        [SerializeField] Image _upgradedDamageValue;

        [Header("Fire Rate")]
        [SerializeField] Image _currentFireRateValue;
        [SerializeField] Image _upgradedFireRateValue;

        [Header("Reload")]
        [SerializeField] Image _currentReloadValue;
        [SerializeField] Image _upgradedReloadValue;

        [Header("Magazine Size")]
        [SerializeField] Image _currentMagazineSizeValue;
        [SerializeField] Image _upgradedMagazineSizeValue;

        private UpgradeType _currentUpgrade;
        private float _damage;
        private float _fireRate;
        private float _reload;
        private float _magazineSize;

        public event Action<float, float, float, float> ValuesSet;

        private void OnEnable()
        {
            SetWeapon(_platesView.CurrentWeapon);
            _upgradePanel.Upgraded += OnUpgraded;
            _platesView.WeaponSelected += OnWeaponSelected;

            foreach (var upgrade in _upgradeTypes)
                upgrade.UpgradeChoosed += OnUpgradeChoosed;

            DisplayCurrentStats();
            DisplayUpgradeValues();
            UpdateBars();
        }

        private void OnDisable()
        {
            _upgradePanel.Upgraded -= OnUpgraded;
            _platesView.WeaponSelected -= OnWeaponSelected;

            foreach (var upgrade in _upgradeTypes)
                upgrade.UpgradeChoosed -= OnUpgradeChoosed;
        }

        private void OnWeaponInitialized()
        {
            DisplayCurrentStats();
            DisplayUpgradeValues();
            UpdateBars();

            _weapon.WeaponInitialized -= OnWeaponInitialized;
        }

        private void OnUpgraded()
        {
            DisplayCurrentStats();
            DisplayUpgradeValues(_currentUpgrade);
            UpdateBars();
        }

        private void OnUpgradeChoosed(UpgradeType upgrade)
        {
            DisplayUpgradeValues(upgrade);
            UpdateBars();
        }

        private void OnWeaponSelected(Weapon weapon)
        {
            SetWeapon(weapon);
            DisplayCurrentStats();
            DisplayUpgradeValues();
            UpdateBars();

            Debug.Log("On Weapon Selected");
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
            var frameUpgrade = _weapon.GetFrameUpgrade();
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

        public void SetWeapon(Weapon weapon)
        {
            _weapon = weapon;
            _weapon.WeaponInitialized += OnWeaponInitialized;
        }

        private void UpdateBars()
        {
            _currentDamageValue.rectTransform.sizeDelta = 
                new Vector2(_weapon.Damage, _currentDamageValue.rectTransform.sizeDelta.y);
            _upgradedDamageValue.rectTransform.sizeDelta = 
                new Vector2(_weapon.Damage + _damage, _upgradedDamageValue.rectTransform.sizeDelta.y);
            _currentFireRateValue.rectTransform.sizeDelta = 
                new Vector2(_weapon.FireRate, _currentFireRateValue.rectTransform.sizeDelta.y);
            _upgradedFireRateValue.rectTransform.sizeDelta = 
                new Vector2(_weapon.FireRate + _fireRate, _upgradedFireRateValue.rectTransform.sizeDelta.y);
            _currentReloadValue.rectTransform.sizeDelta = 
                new Vector2(_weapon.ReloadSpeed, _currentReloadValue.rectTransform.sizeDelta.y);
            _upgradedReloadValue.rectTransform.sizeDelta = 
                new Vector2(_weapon.ReloadSpeed + _reload, _upgradedReloadValue.rectTransform.sizeDelta.y);
            _currentMagazineSizeValue.rectTransform.sizeDelta = 
                new Vector2(_weapon.MagazineSize, _currentMagazineSizeValue.rectTransform.sizeDelta.y);
            _upgradedMagazineSizeValue.rectTransform.sizeDelta = 
                new Vector2(_weapon.MagazineSize + _magazineSize, _upgradedMagazineSizeValue.rectTransform.sizeDelta.y);
        }

    }
}
