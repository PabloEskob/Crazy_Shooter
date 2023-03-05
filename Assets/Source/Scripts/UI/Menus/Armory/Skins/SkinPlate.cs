using Assets.Source.Scripts.Data;
using Assets.Source.Scripts.UI.Menus.Armory.Skins;
using Assets.Source.Scripts.Weapons;
using InfimaGames.LowPolyShooterPack;
using Source.Scripts.Data;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Scripts.UI.Menus.Armory
{
    public class SkinPlate : MonoBehaviour
    {
        [SerializeField] private Image _buttonImage;
        [SerializeField] private Image _frameImage;
        [SerializeField] private Image _purchasedStatusCheckMark;
        [SerializeField] private Image _equippedStatusCheckMark;
        [SerializeField] private SkinPriceData _priceData;

        private Button _button;
        private Weapon _currentWeapon;
        private WeaponSkinsHandler _weaponSkinsHandler;

        public IStorage _storage;
        public int Price { get; private set; }
        public int IndexID { get; private set; }
        public bool IsEquipped { get; private set; }
        public bool IsBought { get; private set; }
        private string NameKey => _buttonImage.sprite.name;
        public string SkinPlateKey => $"{_currentWeapon.name}{IndexID}";
        public SkinPlateData Data { get; private set; }

        public event Action<SkinPlate, int> Choosed;

        public void Init(Weapon weapon, int index, Sprite sprite, IStorage storage)
        {
            Data = new SkinPlateData();
            _storage = storage;
            _currentWeapon = weapon;
            IndexID = index;
            _buttonImage.sprite = sprite;

            if (index >= 0 && index < _priceData.PricecCount)
                Price = _priceData.GetPriceBuyIndex(index);

            OnLoad();
        }

        private void Awake() => _button = GetComponent<Button>();

        private void OnEnable() => _button.onClick.AddListener(OnButtonclick);

        private void OnDisable() => _button.onClick.RemoveListener(OnButtonclick);

        private void OnButtonclick() => Choosed?.Invoke(this, IndexID);
        private void OnLoad()
        {
            if (_storage.HasKeyString(SkinPlateKey))
            {
                SetData(_storage.GetString(SkinPlateKey));
                GetFromData();

                if (IsEquipped)
                    Choosed?.Invoke(this, IndexID);
            }
            else
            {
                SetToData();
                Save();
            }

            ChangeEquippedCheckMarkView();
            ChangePurchadedCheckMarkView(IsBought);
        }

        public void SwitchFrameView(bool value) => _frameImage.enabled = value;

        public void ChangePurchadedCheckMarkView(bool value) => _purchasedStatusCheckMark.enabled = value;

        public void ChangeEquippedCheckMarkView() => _equippedStatusCheckMark.enabled = IsEquipped;

        public void SetData(string name) =>
            Data = name.ToDeserialized<SkinPlateData>();

        public void SetPrice(int price) => Price = price;

        public void ChageEquippedState(bool value)
        {
            IsEquipped = value;
            SetToData();
            Save();
        }

        public void SetIsBought(bool value)
        {
            IsBought = value;
            SetToData();
            Save();
        }

        public void SetToData()
        {
            Data.IsBought = IsBought;
            Data.IsEquipped = IsEquipped;
        }

        public void GetFromData()
        {
            IsBought = Data.IsBought;
            IsEquipped = Data.IsEquipped;
        }

        public void Save()
        {
            _storage.SetString(SkinPlateKey, Data.ToJson());
            _storage.Save();
        }
    }
}
