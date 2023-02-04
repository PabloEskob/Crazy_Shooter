using Assets.Source.Scripts.UI.Menus.Armory.Skins;
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
        [SerializeField] private Image _checkMark;
        [SerializeField] private SkinPriceData _priceData;

        private Button _button;
        private Weapon _currentWeapon;
        private WeaponSkinsHandler _weaponSkinsHandler;

        public int Price { get; private set; }
        public int IndexID { get; private set; }
        public bool IsBought { get; private set; }
        private string NameKey => _buttonImage.sprite.name;

        public event Action<SkinPlate, int> Choosed;

        public void Init(Weapon weapon, int index, Sprite sprite)
        {
            _currentWeapon = weapon;
            IndexID = index;
            _buttonImage.sprite = sprite;

            if (index >= 0 && index < _priceData.PricecCount)
                Price = _priceData.GetPriceBuyIndex(index);
        }

        private void Awake() => _button = GetComponent<Button>();

        private void OnEnable() => _button.onClick.AddListener(OnButtonclick);

        private void OnDisable() => _button.onClick.RemoveListener(OnButtonclick);

        private void OnButtonclick() => Choosed?.Invoke(this, IndexID);

        public void SwitchFrameView(bool value) => _frameImage.enabled = value;

        public void ChangeCheckMarkView(bool value) => _checkMark.enabled = value;

        public void SetIsBought(bool value) => IsBought = value;

        public void SetPrice(int price) => Price = price;
    }
}
