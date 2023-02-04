using Assets.Source.Scripts.UI.Menus.Armory.Skins;
using Assets.Source.Scripts.Weapons;
using InfimaGames.LowPolyShooterPack;
using Source.Scripts.Ui;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Scripts.UI.Menus.Armory
{
    public class SkinsPanel : MonoBehaviour
    {
        [SerializeField] private UpgradeMenu _upgradeMenu;
        [SerializeField] private SkinPlate _template;
        [SerializeField] private Transform _container;
        [SerializeField] private BuySkinButton _buySkinButton;
        [SerializeField] private AdSkinButton _adSkinButton;
        [SerializeField] private SoftCurrencyHolder _currencyHolder;

        private const float PivotValue = 0.5f;
        private const int DefaultSkinIndex = -1;

        private SkinPlate _currentPlate;
        private Sprite _sprite;
        private List<SkinPlate> _skinPlates = new List<SkinPlate>();

        private Button BuySkinUIButton => _buySkinButton.BuySkinUIButton;
        private Button AdSkinUIButton => _adSkinButton.AdSkinUIButton;
        private Weapon Weapon => _upgradeMenu.CurrentWeapon;
        private WeaponSkinsHandler SkinsHandler => Weapon.SkinsHandler;

        private void OnEnable()
        {
            BuySkinUIButton.onClick.AddListener(OnBuySkinButtonClick);
            AdSkinUIButton.onClick.AddListener(OnAdSkinUIButtonClick);
            Fill();
        }

        private void OnDisable()
        {
            BuySkinUIButton.onClick.RemoveListener(OnBuySkinButtonClick);
            AdSkinUIButton.onClick.RemoveListener(OnAdSkinUIButtonClick);
            SkinsHandler.ResetTexture();
            Clear();
        }

        private void Fill()
        {
            if (SkinsHandler != null)
            {
                _sprite = CreateSprite(SkinsHandler.DefaultTexture);
                SkinPlate defaultSkinPlate = CreatePlate(DefaultSkinIndex, _sprite);
                _skinPlates.Add(defaultSkinPlate);

                for (int i = 0; i < SkinsHandler.TextureList.Count; i++)
                {
                    _sprite = CreateSprite(SkinsHandler.TextureList[i]);
                    SkinPlate skinPlate = CreatePlate(i, _sprite);
                    _skinPlates.Add(skinPlate);
                }
            }
            else
            {
                throw new NullReferenceException("SkinHandler is null");
            }
        }

        private SkinPlate CreatePlate(int i, Sprite sprite)
        {
            SkinPlate skinPlate = Instantiate(_template, _container);
            skinPlate.SwitchFrameView(false);
            skinPlate.Init(Weapon, i, sprite);
            skinPlate.Choosed += OnChoosed;
            return skinPlate;
        }

        private Sprite CreateSprite(Texture2D texture) =>
            Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(PivotValue, PivotValue));

        private void Clear()
        {
            foreach (SkinPlate skinPlate in _skinPlates)
            {
                skinPlate.Choosed -= OnChoosed;
                Destroy(skinPlate.gameObject);
            }

            _skinPlates.Clear();
        }

        private void OnChoosed(SkinPlate skinPlate, int index)
        {
            _currentPlate = skinPlate;

            foreach (SkinPlate plate in _skinPlates)
                plate.SwitchFrameView(false);

            _currentPlate.SwitchFrameView(true);

            SwitchButtonsView();

            if (index == DefaultSkinIndex)
                SkinsHandler.SetTexture(SkinsHandler.DefaultTexture);
            else
                SkinsHandler.SetTexture(SkinsHandler.TextureList[index]);

        }

        private void OnBuySkinButtonClick()
        {
            if (_currencyHolder.CheckSolvency(_currentPlate.Price))
            {
                SkinsHandler.ApplyTexture();
                _currencyHolder.Spend(_currentPlate.Price);
                _currentPlate.SetIsBought(true);
                _currentPlate.ChangeCheckMarkView(true);
                _buySkinButton.SwitchButtonInteractable(false);
            }
        }

        private void OnAdSkinUIButtonClick()
        {
            Debug.Log("Advertisement");
            SkinsHandler.ApplyTexture();
            _currentPlate.SetIsBought(true);
            _currentPlate.ChangeCheckMarkView(true);
            _adSkinButton.ChangeScale(!_currentPlate.IsBought);
        }

        private void SwitchButtonsView()
        {
            if (_currentPlate.Price > 0)
            {
                _adSkinButton.gameObject.SetActive(false);
                _buySkinButton.gameObject.SetActive(true);
                _buySkinButton.ChangePriceText(_currentPlate.Price.ToString());
            }
            else
            {
                _adSkinButton.gameObject.SetActive(true);
                _buySkinButton.gameObject.SetActive(false);
            }

            if (_currentPlate.IndexID == DefaultSkinIndex || Weapon.IsBought() == false)
            {
                _adSkinButton.gameObject.SetActive(false);
                _buySkinButton.gameObject.SetActive(false);
            }

            _buySkinButton.SwitchButtonInteractable(!_currentPlate.IsBought);
            _adSkinButton.ChangeScale(!_currentPlate.IsBought);
        }
    }
}
