using Assets.Source.Scripts.UI.Menus.Rewards;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Scripts.UI.Menus.Armory
{
    public class UpgradeButtonAd : AdvertisementButton
    {
        //[SerializeField] private Button _button;
        [SerializeField] private UpgradeHandler _upgradeHandler;


        //protected override void OnEnable()
        //{
        //    _upgradeHandler.Upgraded += OnUpgraded;
        //}

        //protected override void OnDisable()
        //{
        //    _upgradeHandler.Upgraded -= OnUpgraded;
        //}

        //private void OnUpgraded()
        //{
        //    _button.interactable = false;
        //}
    }
}
