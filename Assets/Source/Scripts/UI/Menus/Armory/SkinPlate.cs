using Assets.Source.Scripts.Weapons;
using InfimaGames.LowPolyShooterPack;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Scripts.UI.Menus.Armory
{
    public class SkinPlate : MonoBehaviour
    {
        [SerializeField] private Image _buttonImage;
        [SerializeField] private Image _frameImage;

        private Button _button;
        private Weapon _currentWeapon;
        private WeaponSkinsHandler _weaponSkinsHandler;
        private int _indexID;
        private string NameKey => _buttonImage.sprite.name;

        public event Action<SkinPlate, int> Choosed;

        public void Init(Weapon weapon, int index, Sprite sprite)
        {
            _currentWeapon = weapon;
            _indexID = index;
            _buttonImage.sprite = sprite;
        }

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
            Choosed?.Invoke(this, _indexID);
        }

        public void SwitchFrameView(bool value)
        {
            _frameImage.enabled = value;
        }

    }
}
