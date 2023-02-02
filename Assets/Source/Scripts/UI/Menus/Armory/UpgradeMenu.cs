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
        
        private IStorage _storage;
        private Weapon _currentWeapon;
        
        private const int _defaultWeaponIndex = 0;
        private const float InvokeDelay = 0.1f;

        public UpgradeHandler UpgradeHandler => _upgradeHandler;
        public Weapon CurrentWeapon => _currentWeapon;
        public Inventory Inventory => _inventory;

        private void Awake()
        {
            _inventory.Initialized += OnInitialized;
            _inventory.Init();
        }

        private void OnEnable()
        {
            _upgradePanel.Upgraded += OnUpgraded;
            _exitButton.onClick.AddListener(Hide);
            _weaponPlatesView.WeaponSelected += OnWeaponSelected;
        }

        private void OnDisable()
        {
            _upgradePanel.Upgraded -= OnUpgraded;
            _inventory.Initialized -= OnInitialized;
            _exitButton.onClick.RemoveListener(Hide);
            _weaponPlatesView.WeaponSelected -= OnWeaponSelected;
        }

        private void Start() => _storage = _mainMap.Storage;

        private void OnUpgraded()
        {
            _storage.SetString(_currentWeapon.GetName(), _currentWeapon.GetData().ToJson());
            _storage.Save();
        }

        private void OnWeaponSelected(Weapon weapon)
        {
            _currentWeapon = weapon;
            _weaponHolder.UpdateView(_currentWeapon);
            _upgradeHandler.UpdateWeaponInfo(_currentWeapon);
            //_upgradeHandler.SetCurrentData();
        }

        private void OnInitialized()
        {
            _weaponPlatesView.SetInventory(_inventory);
            _weaponPlatesView.InitPlates();
            _weaponHolder.SetWeapons(_inventory.Weapons.ToList());
            _weaponHolder.SetWeaponIndex(_defaultWeaponIndex);
            _currentWeapon = _weaponHolder.DefaultWeapon;
            _weaponHolder.UpdateView(_currentWeapon);
            _upgradeHandler.UpdateWeaponInfo(_currentWeapon);
            Hide();
        }

        private void Hide() => gameObject.SetActive(false);

        public void Show() => gameObject.SetActive(true);
    }
}
