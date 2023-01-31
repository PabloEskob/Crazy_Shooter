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
        [SerializeField] private float _barMaxValue = 100;
        [SerializeField] private float _maxLength = 200;
        [SerializeField] private float _scalingFactor = 1;

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

        private Weapon _weapon;
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
            if (_currentUpgrade != null)
            {
                DisplayCurrentStats();
                DisplayUpgradeValues(_currentUpgrade);
                UpdateBars();
                Debug.Log(_currentUpgrade.name);
            }
            else
            {
                Debug.Log($"currentUpgrade == null");
            }
        }

        private void OnUpgradeChoosed(UpgradeType upgrade)
        {
            SetUpgrade(upgrade);
            DisplayUpgradeValues(_currentUpgrade);
            UpdateBars();
        }

        private void OnWeaponSelected(Weapon weapon)
        {
            SetWeapon(weapon);
            DisplayCurrentStats();
            DisplayUpgradeValues();
            UpdateBars();
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
            _upgradeDamageValue.text = GetUpgradeValueText(frameUpgrade.Damage);
            _upgradeFireRateValue.text = GetUpgradeValueText(frameUpgrade.FireRate);
            _upgradeReloadValue.text = GetUpgradeValueText(frameUpgrade.Reload);
            _upgradeMagazineSizeValue.text = GetUpgradeValueText(frameUpgrade.MagazineSize);
            SetValues(frameUpgrade.Damage, frameUpgrade.FireRate, frameUpgrade.Reload, frameUpgrade.MagazineSize);
        }

        private string GetUpgradeValueText(float value) => value != 0 ? $" +{value}" : "";

        public void SetUpgrade(UpgradeType upgrade) => _currentUpgrade = upgrade;

        private void DisplayUpgradeValues(UpgradeType upgrade)
        {
            switch (_currentUpgrade)
            {
                case FrameUpgrade:
                    var frameUpgrade = _weapon.GetFrameUpgrade();
                    _upgradeDamageValue.text = GetUpgradeValueText(frameUpgrade.Damage);
                    _upgradeFireRateValue.text = GetUpgradeValueText(frameUpgrade.FireRate);
                    _upgradeReloadValue.text = GetUpgradeValueText(frameUpgrade.Reload);
                    _upgradeMagazineSizeValue.text = GetUpgradeValueText(frameUpgrade.MagazineSize);
                    SetValues(frameUpgrade.Damage, frameUpgrade.FireRate, frameUpgrade.Reload, frameUpgrade.MagazineSize);
                    break;
                case MuzzleUpgrade:
                    var muzzleUpgrade = _weapon.GetMuzzleUpgrade();
                    _upgradeDamageValue.text = GetUpgradeValueText(muzzleUpgrade.Damage);
                    _upgradeFireRateValue.text = GetUpgradeValueText(muzzleUpgrade.FireRate);
                    _upgradeReloadValue.text = GetUpgradeValueText(muzzleUpgrade.Reload);
                    _upgradeMagazineSizeValue.text = GetUpgradeValueText(muzzleUpgrade.MagazineSize);
                    SetValues(muzzleUpgrade.Damage, muzzleUpgrade.FireRate, muzzleUpgrade.Reload, muzzleUpgrade.MagazineSize);
                    break;
                case ScopeUpgrade:
                    var scopeUpgrade = _weapon.GetScopeUpgrade();
                    _upgradeDamageValue.text = GetUpgradeValueText(scopeUpgrade.Damage);
                    _upgradeFireRateValue.text = GetUpgradeValueText(scopeUpgrade.FireRate);
                    _upgradeReloadValue.text = GetUpgradeValueText(scopeUpgrade.Reload);
                    _upgradeMagazineSizeValue.text = GetUpgradeValueText(scopeUpgrade.MagazineSize);
                    SetValues(scopeUpgrade.Damage, scopeUpgrade.FireRate, scopeUpgrade.Reload, scopeUpgrade.MagazineSize);
                    break;
                case BulletsUpgrade:
                    var bulletUpgrade = _weapon.GetBulletsUpgrade();
                    _upgradeDamageValue.text = GetUpgradeValueText(bulletUpgrade.Damage);
                    _upgradeFireRateValue.text = GetUpgradeValueText(bulletUpgrade.FireRate);
                    _upgradeReloadValue.text = GetUpgradeValueText(bulletUpgrade.Reload);
                    _upgradeMagazineSizeValue.text = GetUpgradeValueText(bulletUpgrade.MagazineSize);
                    SetValues(bulletUpgrade.Damage, bulletUpgrade.FireRate, bulletUpgrade.Reload, bulletUpgrade.MagazineSize);
                    break;
                case MagazineUpgrade:
                    var magazineUpgrade = _weapon.GetMagazineUpgrade();
                    _upgradeDamageValue.text = GetUpgradeValueText(magazineUpgrade.Damage);
                    _upgradeFireRateValue.text = GetUpgradeValueText(magazineUpgrade.FireRate);
                    _upgradeReloadValue.text = GetUpgradeValueText(magazineUpgrade.Reload);
                    _upgradeMagazineSizeValue.text = GetUpgradeValueText(magazineUpgrade.MagazineSize);
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

        private void SetCurrentValueBar(Image currentValuesBar, float currentValue)
        {
            currentValuesBar.rectTransform.sizeDelta =
                new Vector2(GetNormalizedValues(currentValue) * _scalingFactor, _currentDamageValue.rectTransform.sizeDelta.y);
        }

        private void SetUpgradeValueBar(Image upgradeValueBar, float upgradeValue)
        {
            upgradeValueBar.rectTransform.sizeDelta =
                new Vector2(GetNormalizedValues(upgradeValue) * _scalingFactor, _currentDamageValue.rectTransform.sizeDelta.y);
        }

        private float GetNormalizedValues(float value) => value / _barMaxValue * _maxLength;

        private void CheckValues(float currentValue, float upgradeValue)
        {
            if (currentValue > _barMaxValue)
                currentValue = _barMaxValue;

            if (upgradeValue > _barMaxValue - currentValue)
                upgradeValue = _barMaxValue - currentValue;

            if (currentValue + upgradeValue > _barMaxValue)
                upgradeValue = _barMaxValue - currentValue;
        }

        private void UpdateBars()
        {
            CheckValues(_weapon.Damage, _damage);
            SetCurrentValueBar(_currentDamageValue, _weapon.Damage);
            SetUpgradeValueBar(_upgradedDamageValue, _damage);

            CheckValues(_weapon.FireRate, _fireRate);
            SetCurrentValueBar(_currentFireRateValue, _weapon.FireRate);
            SetUpgradeValueBar(_upgradedFireRateValue, _fireRate);

            CheckValues(_weapon.ReloadSpeed, _reload);
            SetCurrentValueBar(_currentReloadValue, _weapon.ReloadSpeed);
            SetUpgradeValueBar(_upgradedReloadValue, _reload);

            CheckValues(_weapon.MagazineSize, _magazineSize);
            SetCurrentValueBar(_currentMagazineSizeValue, _weapon.MagazineSize);
            SetUpgradeValueBar(_upgradedMagazineSizeValue, _magazineSize);
        }

    }
}
