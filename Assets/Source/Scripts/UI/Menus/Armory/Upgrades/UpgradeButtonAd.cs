using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Scripts.UI.Menus.Armory
{
    public class UpgradeButtonAd : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private UpgradeHandler _upgradeHandler;

        public Button Button => _button;

        private void OnEnable()
        {
            _upgradeHandler.Upgraded += OnUpgraded;
        }

        private void OnDisable()
        {
            _upgradeHandler.Upgraded -= OnUpgraded;
        }

        private void OnUpgraded()
        {
            _button.interactable = false;
        }
    }
}
