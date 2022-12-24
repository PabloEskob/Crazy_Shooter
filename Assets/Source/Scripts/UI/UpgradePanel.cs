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

    public event Action<Weapon> WeaponSet;

    private void OnEnable()
    {
        foreach (UpgradeType button in _upgradeTypeButtons) 
            button.UpgradeChoosed += OnUpgradeChoosed;
    }

    private void OnDisable()
    {
        foreach (UpgradeType button in _upgradeTypeButtons) 
            button.UpgradeChoosed -= OnUpgradeChoosed;
    }

    private void OnUpgradeChoosed(UpgradeType upgradeType)
    {
        foreach (UpgradeType typeButton in _upgradeTypeButtons) 
            typeButton.SwitchButtonState(false);

        upgradeType.SwitchButtonState(true);
    }

    public void SetWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
        WeaponSet?.Invoke(_currentWeapon);
    } 
}