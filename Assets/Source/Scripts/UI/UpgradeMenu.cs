using System;
using System.Collections.Generic;
using System.Linq;
using InfimaGames.LowPolyShooterPack;
using Source.Infrastructure;
using Source.Scripts.Data;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.Ui
{
    public class UpgradeMenu : MonoBehaviour
    {
        [SerializeField] private Button _exitButton;
        [SerializeField] private WeaponHolder _weaponHolder;
        [SerializeField] private WeaponPlatesView _weaponPlatesView;
        [SerializeField] private UpgradePanel _upgradePanel;
        [SerializeField] private Inventory _inventory;
        
        private IStorage _storage;
        private Weapon _currentWeapon;
        
        private const int _defaultWeaponIndex = 0;

        public Weapon CurrentWeapon => _currentWeapon;
        public Inventory Inventory => _inventory;

        private void Awake()
        {
            _upgradePanel.Upgraded -= OnUpgraded;
            _inventory.Initialized += OnInitialized;
            _inventory.Init();
        }

        private void OnEnable()
        {
            _upgradePanel.Upgraded += OnUpgraded;
            _exitButton.onClick.AddListener(Hide);
            _weaponPlatesView.WeaponSelected += OnWeaponSelected;
        }

        private void OnUpgraded()
        {
            _storage = AllServices.Container.Single<IStorage>();
            _storage.SetString(_currentWeapon.GetName(), _currentWeapon.GetData().ToJson());
            _storage.Save();
        }

        private void OnDisable()
        {
            _inventory.Initialized -= OnInitialized;
            _exitButton.onClick.RemoveListener(Hide);
            _weaponPlatesView.WeaponSelected -= OnWeaponSelected;
        }

        private void Hide() =>
            gameObject.SetActive(false);

        private void OnWeaponSelected(Weapon weapon)
        {
            _currentWeapon = weapon;
            _weaponHolder.UpdateView(_currentWeapon);
            _upgradePanel.SetWeapon(_currentWeapon);
        }

        private void OnInitialized()
        {
            _weaponPlatesView.SetInventory(_inventory);
            _weaponPlatesView.InitPlates();
            _weaponHolder.SetWeapons(_inventory.Weapons.ToList());
            _weaponHolder.SetWeaponIndex(_defaultWeaponIndex);
            _currentWeapon = _weaponHolder.DefaultWeapon;
            _weaponHolder.UpdateView(_currentWeapon);
            _upgradePanel.SetWeapon(_currentWeapon);
            gameObject.SetActive(false);
        }

        public void Show() =>
            gameObject.SetActive(true);

        

    }
}
