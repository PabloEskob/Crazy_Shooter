using Assets.Source.Scripts.UI.Menus.Armory;
using InfimaGames.LowPolyShooterPack;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.Ui
{
    public class WeaponStatsDisplay : MonoBehaviour
    {
        [SerializeField] private UpgradeHandler _upgradeHandler;
        [SerializeField] private WeaponPlatesView _platesView;

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

        private Weapon _weapon => _upgradeHandler.GetWeapon();
        private float _damage;
        private float _fireRate;
        private float _reload;
        private float _magazineSize;

        public event Action<float, float, float, float> ValuesSet;

        private void OnEnable()
        {
            _upgradeHandler.UpgradeSelected += OnUpgradeSelected;
            _upgradeHandler.WeaponSetted += OnWeaponSetted;
            _upgradeHandler.Upgraded += OnUpgraded;
        }

        private void OnDisable()
        {
            _upgradeHandler.UpgradeSelected -= OnUpgradeSelected;
            _upgradeHandler.WeaponSetted -= OnWeaponSetted;
            _upgradeHandler.Upgraded -= OnUpgraded;
        }

        private void Start()
        {
            DisplayCurrentStats();
            DisplayUpgradeValues();
            UpdateBars();
        }

        private void OnWeaponSetted(Weapon weapon)
        {
            _upgradeHandler.SetCurrentData();
            DisplayCurrentStats();
            DisplayUpgradeValues();
            UpdateBars();
        }

        private void OnUpgradeSelected(UpgradeType upgrade)
        {
            DisplayUpgradeValues();
            UpdateBars();
        }

        private void OnUpgraded()
        {
            DisplayCurrentStats();
            DisplayUpgradeValues();
            UpdateBars();
        }

        private void DisplayCurrentStats()
        {
            var weapon = _upgradeHandler.GetWeapon();
            _damageValue.text = weapon.Damage.ToString();
            _fireRateValue.text = weapon.FireRate.ToString();
            _reloadValue.text = weapon.ReloadSpeed.ToString();
            _magazineSizeValue.text = weapon.MagazineSize.ToString();
        }

        private void DisplayUpgradeValues()
        {
            var upgradeData = _upgradeHandler.GetWeaponUpgradeData();
            _upgradeDamageValue.text = GetUpgradeValueText(upgradeData.Damage);
            _upgradeFireRateValue.text = GetUpgradeValueText(upgradeData.FireRate);
            _upgradeReloadValue.text = GetUpgradeValueText(upgradeData.Reload);
            _upgradeMagazineSizeValue.text = GetUpgradeValueText(upgradeData.MagazineSize);
            SetValues(upgradeData.Damage, upgradeData.FireRate, upgradeData.Reload, upgradeData.MagazineSize);
        }

        private string GetUpgradeValueText(float value) => value != 0 ? $" +{value}" : "";

        private void SetValues(float damage, float fireRate, float reload, float magazineSize)
        {
            _damage = damage;
            _fireRate = fireRate;
            _reload = reload;
            _magazineSize = magazineSize;

            ValuesSet?.Invoke(_damage, _fireRate, _reload, _magazineSize);
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
