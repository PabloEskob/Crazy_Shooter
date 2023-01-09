using System;
using System.Collections;
using System.Collections.Generic;
using InfimaGames.LowPolyShooterPack;
using Source.Scripts.Ui;
using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] private List<UpgradeType> _upgradeTypeButtons;
    private Weapon _currentWeapon;

    public Weapon CurrentWeapon => _currentWeapon;

    public event Action<Weapon> WeaponSet;

    private void OnEnable()
    {
        foreach (UpgradeType button in _upgradeTypeButtons)
            button.UpgradeChoosed += OnUpgradeChoosed;

        WeaponSet += OnWeaponSet;
    }

    private void OnDisable()
    {
        foreach (UpgradeType button in _upgradeTypeButtons)
            button.UpgradeChoosed -= OnUpgradeChoosed;

        WeaponSet -= OnWeaponSet;
    }

    private void OnUpgradeChoosed(UpgradeType upgradeType)
    {
        foreach (UpgradeType typeButton in _upgradeTypeButtons)
        {
            typeButton.SwitchButtonState(false);
            typeButton.SetText();
        }

        upgradeType.SwitchButtonState(true);
        upgradeType.SetText();
    }

    public void SetWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
        WeaponSet?.Invoke(_currentWeapon);
    }

    private void OnWeaponSet(Weapon weapon)
    {
        foreach (var button in _upgradeTypeButtons)
            button.SwitchButtonInteractivity(weapon.IsBought());
    }
}