using System;
using System.Collections.Generic;
using InfimaGames.LowPolyShooterPack;
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
        
        private Weapon _currentWeapon;
        
        private const int _defaultWeaponIndex = 0;

        public Weapon CurrentWeapon => _currentWeapon; 

        private void Awake()
        {
            _weaponHolder.SetWeaponIndex(_defaultWeaponIndex);
            _currentWeapon = _weaponHolder.DefaultWeapon;
            _weaponHolder.UpdateView(_currentWeapon);
            _upgradePanel.SetWeapon(_currentWeapon);
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _exitButton.onClick.AddListener(Hide);
            _weaponPlatesView.WeaponSelected += OnWeaponSelected;
        }

        private void OnDisable()
        {
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

        public void Show() =>
            gameObject.SetActive(true);
    }
}
