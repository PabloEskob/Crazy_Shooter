using System;
using InfimaGames.LowPolyShooterPack;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Source.Scripts.Ui
{
    public abstract class WeaponPlate : MonoBehaviour
    {
        [SerializeField] private Button _weaponButton;
        [SerializeField] private Weapon _weapon;
        [SerializeField] private Image _weaponImage;

        [Header("Button Color Settings")]
        [SerializeField] private Color32 _activeButtonColor = new Color(100, 58, 100);
        [SerializeField] private Color32 _inactiveButtonColor = new Color(36, 16, 100);

        private bool _isSelected;

        public Weapon Weapon => _weapon;

        public event Action<WeaponPlate, Weapon> WeaponSelected;

        private void OnEnable() =>
            _weaponButton.onClick.AddListener(OnButtonClick);

        private void OnDisable() =>
            _weaponButton.onClick.RemoveListener(OnButtonClick);

        private void OnButtonClick() =>
            WeaponSelected?.Invoke(this, _weapon);

        private ColorBlock GetColorBlock(Color color)
        {
            ColorBlock colorBlock = _weaponButton.colors;
            colorBlock.normalColor = color;
            colorBlock.highlightedColor = color;
            colorBlock.selectedColor = color;
            colorBlock.pressedColor = color;
            colorBlock.disabledColor = color;
            return colorBlock;
        }

        private void ChangeButtonView() => 
            _weaponButton.colors = GetColorBlock(_isSelected ? _activeButtonColor : _inactiveButtonColor);

        public void SwitchButtonState(bool value)
        {
            _isSelected = value;
            _weaponButton.interactable = !value;
            ChangeButtonView();
        }

        public void SetWeapon(Weapon weapon)
        {
            _weapon = weapon;
            _weaponImage.sprite = weapon.GetIcon();
        }

        public void ShowWeapon() =>
            _weapon.gameObject.SetActive(true);

        public void HideWeapon() =>
            _weapon.gameObject.SetActive(false);
    }
}