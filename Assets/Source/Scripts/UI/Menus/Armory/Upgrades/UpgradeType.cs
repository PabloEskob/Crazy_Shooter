using System;
using Assets.Source.Scripts.UI.Menus.Armory;
using InfimaGames.LowPolyShooterPack;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.Ui
{
    public abstract class UpgradeType : MonoBehaviour
    {
        [SerializeField] protected UpgradeHandler UpgradeHandler;
        [SerializeField] protected Text UpgradeName;
        [SerializeField] protected Button UpgradeButton;
        [SerializeField] protected Text ButtonText;
        [SerializeField] protected Text LevelText;

        protected Weapon Weapon => UpgradeHandler.GetWeapon();
        protected bool IsUpgradeChoosed;

        public event Action<UpgradeType> UpgradeChoosed;

        private void OnEnable()
        {
            UpgradeHandler.WeaponSetted += OnWeaponSet;
            UpgradeButton.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            UpgradeHandler.WeaponSetted -= OnWeaponSet;
            UpgradeButton.onClick.RemoveListener(OnButtonClick);
        }

        private void OnUpgraded() => 
            SetText();

        protected abstract void OnButtonClick();
        protected abstract void OnWeaponSet(Weapon weapon);
        public abstract void SetText();

        public void SendEvent() => 
            UpgradeChoosed?.Invoke(this);

        public void SwitchButtonState(bool value) => 
            IsUpgradeChoosed = value;

        public void SwitchButtonInteractivity(bool value) => 
            UpgradeButton.interactable = value;
    }
}