using System;
using System.Collections;
using System.Collections.Generic;
using InfimaGames.LowPolyShooterPack;
using Source.Scripts.Ui;
using UnityEngine;

public class WeaponPlatesView : MonoBehaviour
{
    [SerializeField] private List<WeaponPlate> _plates;

    private int _defaultWeaponIndex;

    public Action<Weapon> WeaponSelected;
    
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

    private void Start() => 
        _plates[_defaultWeaponIndex].SwitchButtonState(true);

    private void OnWeaponSelected(WeaponPlate plate, Weapon weapon)
    {
        foreach (var weaponPlate in _plates) 
            weaponPlate.SwitchButtonState(false);

        plate.SwitchButtonState(true);
        WeaponSelected?.Invoke(weapon);
    }

    public void SetDefaultIndex(int index) => 
        _defaultWeaponIndex = index;
}
