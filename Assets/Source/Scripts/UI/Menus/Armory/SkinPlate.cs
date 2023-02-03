using InfimaGames.LowPolyShooterPack;
using Source.Scripts.Ui;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Source.Scripts.UI.Menus.Armory
{
    public class SkinPlate : MonoBehaviour
    {
        [SerializeField] private UpgradeMenu _upgradeMenu;
        [SerializeField] private Texture2D _skinTexture;

        private Button _button;
        private Weapon _currentWeapon => _upgradeMenu.CurrentWeapon;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonclick);
        }


        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonclick);
        }

        private void OnButtonclick()
        {

        }

    }
}
