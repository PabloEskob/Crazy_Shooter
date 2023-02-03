using InfimaGames.LowPolyShooterPack;
using Source.Scripts.Ui;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Scripts.UI.Menus.Armory
{
    public class SkinsPanel : MonoBehaviour
    {
        [SerializeField] private UpgradeMenu _upgradeMenu;
        [SerializeField] private SkinPlate _template;
        [SerializeField] private Transform _container;

        private Sprite _sprite;
        private float _pivotValue = 0.5f;
        private int _defaultSkinIdex = -1;
        private List<SkinPlate> _skinPlates = new List<SkinPlate>();

        private Weapon _weapon => _upgradeMenu.CurrentWeapon;

        private void OnEnable()
        {
            Fill();
        }

        private void OnDisable()
        {
            Clear();
        }

        private void Fill()
        {
            if (_weapon.SkinsHandler != null)
            {
                _sprite = CreateSprite(_weapon.SkinsHandler.DefaultTexture);
                SkinPlate defaultSkinPlate = CreatePlate(_defaultSkinIdex, _sprite);
                _skinPlates.Add(defaultSkinPlate);

                for (int i = 0; i < _weapon.SkinsHandler.TextureList.Count; i++)
                {
                    _sprite = CreateSprite(_weapon.SkinsHandler.TextureList[i]);
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
            skinPlate.Init(_weapon, i, sprite);
            skinPlate.Choosed += OnChoosed;
            return skinPlate;
        }

        private Sprite CreateSprite(Texture2D texture)
        {
            //Texture2D texture = _weapon.SkinsHandler.TextureList[i];
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(_pivotValue, _pivotValue));
        }

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
            foreach (SkinPlate plate in _skinPlates)
                plate.SwitchFrameView(false);

            skinPlate.SwitchFrameView(true);

            if (index == _defaultSkinIdex)
                _weapon.SkinsHandler.SetTexture(_weapon.SkinsHandler.DefaultTexture);
            else
                _weapon.SkinsHandler.SetTexture(_weapon.SkinsHandler.TextureList[index]);

        }
    }
}
