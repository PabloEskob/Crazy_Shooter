using System.Collections;
using System.Collections.Generic;
using InfimaGames.LowPolyShooterPack;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private List<Weapon> weapons;

    private int _defaultWeaponIndex;
    
    public Weapon DefaultWeapon => weapons[_defaultWeaponIndex];
    
    private void HideAllWeapons()
    {
        foreach (Weapon weapon in weapons) 
            weapon.gameObject.SetActive(false);
    }

    private void ShowWeapon(Weapon selectedWeapon)
    {
        foreach (Weapon weapon in weapons)
            if (weapon == selectedWeapon)
                weapon.gameObject.SetActive(true);
    }

    public void UpdateView(Weapon selectedWeapon)
    {
        HideAllWeapons();
        ShowWeapon(selectedWeapon);
    }

    public void SetWeaponIndex(int index) => 
        _defaultWeaponIndex = index;
}
