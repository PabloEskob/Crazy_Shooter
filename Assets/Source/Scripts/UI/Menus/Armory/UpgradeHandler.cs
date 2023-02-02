using InfimaGames.LowPolyShooterPack;
using Source.Scripts.Data;
using Source.Scripts.StaticData;
using Source.Scripts.Ui;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Scripts.UI.Menus.Armory
{
    public class UpgradeHandler : MonoBehaviour
    {
        [SerializeField] private List<UpgradeType> _upgradeTypes;

        private Weapon _weapon;
        private UpgradeType _defaultUpgradeType;
        private UpgradeType _currentUpgradeType;
        private WeaponUpgradeData _currentWeaponUpgradeData;

        public event Action<UpgradeType> UpgradeSetted;
        public event Action<Weapon> WeaponSetted;
        public event Action<UpgradeType> UpgradeSelected;
        public event Action Upgraded;
        public event Action Bought;

        private void OnEnable()
        {
            foreach (UpgradeType type in _upgradeTypes)
                type.UpgradeChoosed += OnUpgradeChoosed;
        }

        private void OnDisable()
        {
            foreach (UpgradeType type in _upgradeTypes)
                type.UpgradeChoosed -= OnUpgradeChoosed;
        }

        private void Start()
        {
            _defaultUpgradeType = _upgradeTypes[0];
            OnUpgradeChoosed(_defaultUpgradeType);
        }

        private void SetUpgradeType(UpgradeType upgrade)
        {
            _currentUpgradeType = upgrade;
            UpgradeSetted?.Invoke(_currentUpgradeType);
        }

        public UpgradeType GetUpgradeType() =>
            _currentUpgradeType;

        public void SetWeaponUpgradeData(WeaponUpgradeData weaponUpgradeData) =>
            _currentWeaponUpgradeData = weaponUpgradeData;

        public WeaponUpgradeData GetWeaponUpgradeData() =>
            _currentWeaponUpgradeData;

        public void UpdateWeaponInfo(Weapon weapon)
        {
            _weapon = weapon;
            OnUpgradeChoosed(_upgradeTypes[0]);
            SetCurrentData();
            WeaponSetted?.Invoke(_weapon);
        }

        public Weapon GetWeapon() => _weapon;

        private void OnUpgradeChoosed(UpgradeType type)
        {
            foreach (UpgradeType upgradeType in _upgradeTypes)
            {
                upgradeType.SwitchButtonState(false);
                upgradeType.SetText();
            }

            type.SwitchButtonState(true);
            type.SetText();


            SetUpgradeType(type);
            SetCurrentData();
            UpgradeSelected?.Invoke(GetUpgradeType());
        }

        public void SetCurrentData()
        {
            switch (GetUpgradeType())
            {
                case FrameUpgrade:
                    SetWeaponUpgradeData(_weapon.GetFrameUpgrade());
                    break;
                case MuzzleUpgrade:
                    SetWeaponUpgradeData(_weapon.GetMuzzleUpgrade());
                    break;
                case ScopeUpgrade:
                    SetWeaponUpgradeData(_weapon.GetScopeUpgrade());
                    break;
                case BulletsUpgrade:
                    SetWeaponUpgradeData(_weapon.GetBulletsUpgrade());
                    break;
                case MagazineUpgrade:
                    SetWeaponUpgradeData(_weapon.GetMagazineUpgrade());
                    break;
            }
        }

        public void Upgrade(float damage, float fireRate, float reloadSpeed, float magazineSize)
        {
            _weapon.Upgrade(damage, fireRate, reloadSpeed, magazineSize);
            _weapon.UpdateStatsToData();
            SetCurrentData();
            GetUpgradeType().SetText();
            Upgraded?.Invoke();
        }

        public void Buy()
        {
            _weapon.SetIsBought();
            _weapon.SetBoolToData();
            Bought?.Invoke();
        }
    }
}
