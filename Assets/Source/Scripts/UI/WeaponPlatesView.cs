using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using InfimaGames.LowPolyShooterPack;
using Source.Scripts.Ui;
using UnityEngine;

public class WeaponPlatesView : MonoBehaviour
{
    [SerializeField] private WeaponPlate _plateTemplate;
    [SerializeField] private Transform _container;

    private List<WeaponPlate> _plates = new List<WeaponPlate>();
    private Inventory _inventory;
    private int _defaultWeaponIndex;

    public IEnumerable<WeaponPlate> Plates => _plates;

    public event Action<Weapon> WeaponSelected;

    //private void Awake()
    //{
    //    InitPlates();
    //}

    private void OnEnable()
    {
        foreach (WeaponPlate weaponPlate in _plates) 
            weaponPlate.WeaponSelected += OnWeaponSelected;
    }

    private void OnDisable()
    {
        foreach (WeaponPlate weaponPlate in _plates) 
            weaponPlate.WeaponSelected -= OnWeaponSelected;
    }

    private void Start()
    {
    }

    private void OnWeaponSelected(WeaponPlate plate, Weapon weapon)
    {
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
            var plate = Instantiate(_plateTemplate, _container);
            plate.SetWeapon(weapon);
            _plates.Add(plate);
        }

        _plates[_defaultWeaponIndex].SwitchButtonState(true);

    }
}
