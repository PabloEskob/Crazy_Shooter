using System.Collections;
using System.Collections.Generic;
using System.Linq;
using InfimaGames.LowPolyShooterPack;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weapons;

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
                weapon.transform.localScale = new Vector3(1000f, 1000f, 1000f);
                weapon.transform.localPosition = new Vector3(0, 0, -1000f);
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
