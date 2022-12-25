using System;
using InfimaGames.LowPolyShooterPack;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Source.Scripts.Ui
{
    public abstract class UpgradeType : MonoBehaviour
    {
        [SerializeField] protected UpgradePanel UpgradePanel;
        [SerializeField] protected string UpgradeName;
        [SerializeField] protected Button UpgradeButton;
        [SerializeField] protected TMP_Text ButtonText;

        protected Weapon Weapon;
        protected bool IsUpgradeChoosed;

        public event Action<UpgradeType> UpgradeChoosed;

        private void OnEnable()
        { 
            UpgradePanel.WeaponSet += OnWeaponSet;
            UpgradeButton.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            UpgradePanel.WeaponSet -= OnWeaponSet;
            UpgradeButton.onClick.RemoveListener(OnButtonClick);
        }

        protected abstract void OnButtonClick();
        protected abstract void OnWeaponSet(Weapon weapon);

        public abstract void SetText();

        protected void SendEvent() => 
            UpgradeChoosed?.Invoke(this);

        public void SwitchButtonState(bool value) => 
            IsUpgradeChoosed = value;
    }
}