using System;
using InfimaGames.LowPolyShooterPack;
using TMPro;
using UnityEngine;

public class BuyUpgradeButton : MonoBehaviour
{
    [SerializeField] private UpgradePanel _upgradePanel;
    [SerializeField] private TMP_Text _price;

    private void OnEnable() => 
        _upgradePanel.WeaponSet += OnWeaponSet;

    private void OnDisable() => 
        _upgradePanel.WeaponSet -= OnWeaponSet;

    private void OnWeaponSet(Weapon weapon) => 
        SetPriceText(5);


    private void SetPriceText(int price) => 
        _price.text = price.ToString();
}