using System.Collections;
using System.Collections.Generic;
using System.Linq;
using InfimaGames.LowPolyShooterPack;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    private List<Weapon> _weapons;
    private Vector3 _weaponScale = new Vector3(1000f, 1000f, 1000f);
    private Vector3 _weaponPosition = new Vector3(0, 0, -1000f);
    private int _defaultWeaponIndex;
    
    public Weapon DefaultWeapon => _weapons[_defaultWeaponIndex];

    private void HideAllWeapons()
    {
        foreach (Weapon weapon in _weapons) 
            weapon.gameObject.SetActive(false);
    }

    private void ShowWeapon(Weapon selectedWeapon)
    {
        foreach (Weapon weapon in _weapons)
            if (weapon == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                weapon.transform.localScale = _weaponScale;
                weapon.transform.localPosition = _weaponPosition;
            }
    }

    public void UpdateView(Weapon selectedWeapon)
    {
        HideAllWeapons();
        ShowWeapon(selectedWeapon);
    }

    public void SetWeaponIndex(int index) => 
        _defaultWeaponIndex = index;

    public void SetWeapons(List<Weapon> weapons) => 
        _weapons = weapons;
}
