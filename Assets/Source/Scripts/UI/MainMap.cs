using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Source.Scripts.Ui
{
    public class MainMap : MonoBehaviour
    {
        [SerializeField] private Button _upgradeMenuButton;
        [SerializeField] private UpgradeMenu _upgradeMenu;

        private void OnEnable()
        {
            _upgradeMenuButton.onClick.AddListener(OpenUpgradeMenu);
        }

        private void OnDisable()
        {
            _upgradeMenuButton.onClick.RemoveListener(OpenUpgradeMenu);
        }

        private void OpenUpgradeMenu()
        {
            _upgradeMenu.Show();
        }
    }
}