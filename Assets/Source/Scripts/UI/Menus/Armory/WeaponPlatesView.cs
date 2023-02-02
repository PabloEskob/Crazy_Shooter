using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public IEnumerable<WeaponPlate> Plates => _plates;

    public Weapon CurrentWeapon { get; private set; }

    public event Action<Weapon> WeaponSelected;

    private void OnEnable()
    {
        foreach (WeaponPlate weaponPlate in _plates)
            weaponPlate.WeaponSelected += OnWeaponSelected;

        _upgradeHandler.Bought += OnBought;
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
}
