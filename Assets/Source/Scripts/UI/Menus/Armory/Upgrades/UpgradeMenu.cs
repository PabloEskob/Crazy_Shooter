using System;
using System.Linq;
using Assets.Source.Scripts.UI.Menus.Armory;
using InfimaGames.LowPolyShooterPack;
using Source.Scripts.Data;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.Ui
{
    public class UpgradeMenu : MonoBehaviour
    {
        [SerializeField] private UpgradeHandler _upgradeHandler;
        [SerializeField] private Button _exitButton;
        [SerializeField] private WeaponHolder _weaponHolder;
        [SerializeField] private WeaponPlatesView _weaponPlatesView;
        [SerializeField] private UpgradeViewPanel _upgradePanel;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private MainMap _mainMap;
        
        
        private const int _defaultWeaponIndex = 0;
        private const float InvokeDelay = 0.1f;

        public Weapon CurrentWeapon { get; private set; }
        public IStorage Storage { get; private set; }
        public UpgradeHandler UpgradeHandler => _upgradeHandler;
        public Inventory Inventory => _inventory;

        public event Action Activated;

        private void Awake()
        {
            _inventory.Initialized += OnInitialized;
            _inventory.Init();
        }

        private void OnEnable()
        {
            _upgradeHandler.Upgraded += OnUpgraded;
            _upgradeHandler.Bought += OnBought;
            _exitButton.onClick.AddListener(OnCloseButtionClick);
            _weaponPlatesView.WeaponSelected += OnWeaponSelected;
            Activated?.Invoke();
        }

        private void OnDisable()
        {
            _upgradeHandler.Upgraded -= OnUpgraded;
            _upgradeHandler.Bought -= OnBought;
            _inventory.Initialized -= OnInitialized;
            _exitButton.onClick.RemoveListener(OnCloseButtionClick);
            _weaponPlatesView.WeaponSelected -= OnWeaponSelected;
        }

        private void Start() => Storage = _mainMap.Storage;

        private void Save()
        {
            Storage.SetString(CurrentWeapon.GetName(), CurrentWeapon.GetData().ToJson());
            Storage.Save();
        }

        private void OnWeaponSelected(Weapon weapon)
        {
            CurrentWeapon = weapon;
            _weaponHolder.UpdateView(CurrentWeapon);
            _upgradeHandler.UpdateWeaponInfo(CurrentWeapon);
        }

        private void OnInitialized()
        {
            _weaponPlatesView.SetInventory(_inventory);
            _weaponPlatesView.InitPlates();
            _weaponHolder.SetWeapons(_inventory.Weapons.ToList());
            _weaponHolder.SetWeaponIndex(_defaultWeaponIndex);
            CurrentWeapon = _weaponHolder.DefaultWeapon;
            _weaponHolder.UpdateView(CurrentWeapon);
            _upgradeHandler.UpdateWeaponInfo(CurrentWeapon);
            Hide();
        }

        private void OnCloseButtionClick()
        {
            Storage.Save();
            Hide();
        }

        private void OnUpgraded() => Save();

        private void OnBought() => Save();

        private void Hide() => gameObject.SetActive(false);

        public void Show() => gameObject.SetActive(true);
    }
}
