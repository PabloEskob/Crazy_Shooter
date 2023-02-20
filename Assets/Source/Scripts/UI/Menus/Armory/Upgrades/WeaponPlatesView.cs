using System;
using System.Collections.Generic;
using Assets.Source.Scripts.UI.Menus.Armory;
using InfimaGames.LowPolyShooterPack;
using Source.Scripts.Ui;
using UnityEngine;

public class WeaponPlatesView : MonoBehaviour
{
    [SerializeField] private WeaponPlate _plateTemplate;
    [SerializeField] private Transform _container;
    [SerializeField] private UpgradeHandler _upgradeHandler;

    private List<WeaponPlate> _plates = new List<WeaponPlate>();
    private Inventory _inventory;
    private int _defaultWeaponIndex;
    private WeaponPlate _currentPlate;

    public bool CanReload { get; private set; }
    public IEnumerable<WeaponPlate> Plates => _plates;
    public Weapon CurrentWeapon { get; private set; }

    public event Action<Weapon> WeaponSelected;

    private void OnEnable()
    {
        foreach (WeaponPlate weaponPlate in _plates)
            weaponPlate.WeaponSelected += OnWeaponSelected;

        _upgradeHandler.Bought += OnBought;
        _container.localPosition = Vector3.zero;
    }

    private void OnDisable()
    {
        foreach (WeaponPlate weaponPlate in _plates)
            weaponPlate.WeaponSelected -= OnWeaponSelected;

        _upgradeHandler.Bought -= OnBought;
    }

    private void OnWeaponSelected(WeaponPlate plate, Weapon weapon)
    {
        _currentPlate = plate;

        foreach (var weaponPlate in _plates)
            weaponPlate.SwitchButtonState(false);

        plate.SwitchButtonState(true);
        WeaponSelected?.Invoke(weapon);
    }

    public void SetDefaultIndex(int index) =>
        _defaultWeaponIndex = index;

    public void SetInventory(Inventory inventory) =>
        _inventory = inventory;

    public void InitPlates()
    {
        foreach (var weapon in _inventory.Weapons)
        {
            if (weapon.GetCollectedState())
            {
                var plate = Instantiate(_plateTemplate, _container);
                plate.SetWeapon(weapon);
                plate.SwitchButtonState(false);
                _plates.Add(plate);
            }
        }

        _plates[_defaultWeaponIndex].SwitchButtonState(true);
        CurrentWeapon = _plates[_defaultWeaponIndex].Weapon;

    }

    private void OnBought() => _currentPlate.SwitchButtonState(true);

    public void Clear()
    {
        foreach (WeaponPlate plate in _plates)
            Destroy(plate.gameObject);

        _plates.Clear();
    }

    public void AllowReload() => CanReload = true;
}
