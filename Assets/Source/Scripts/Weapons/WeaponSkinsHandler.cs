﻿using InfimaGames.LowPolyShooterPack;
using Source.Infrastructure;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Scripts.Weapons
{
    public class WeaponSkinsHandler : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;
        [SerializeField] private Material[] _weaponMaterials;
        [SerializeField] private List<Texture2D> _textureList;
        [SerializeField] private Texture2D _defaultTexture;

        private string TextureIndexKey;
        private IStorage _storage;

        public Texture2D CurrentTexture { get; private set; }
        public Texture2D DefaultTexture => _defaultTexture;
        public IReadOnlyList<Texture2D> TextureList => _textureList;
        public int CurrentIndex { get; private set; }


        private void Awake()
        {
            TextureIndexKey = $"{_weapon.GetName()}_currentTextureKey;";

            _storage = AllServices.Container.Single<IStorage>();

            if (_storage.HasKeyInt(TextureIndexKey))
            {
                SetTexture(GetTextureByIndex(_storage.GetInt(TextureIndexKey)));
                ApplyTexture();
            }
        }

        public void SetTexture(Texture2D texture)
        {

            foreach (Material material in _weaponMaterials)
                material.mainTexture= texture;
        }

        public void ApplyTexture()
        {
            CurrentTexture = (Texture2D)_weaponMaterials[0].mainTexture;
            CurrentIndex = GetTextureIndexByName(CurrentTexture.name);
            _storage.SetInt(TextureIndexKey, CurrentIndex);
            _storage.Save();
        }

        public void ResetTexture()
        {
            if (CurrentTexture != null)
                SetTexture(CurrentTexture);
            else
                SetTexture(DefaultTexture);
        }

        public Texture2D GetTextureByIndex(int index) => _textureList[index];

        private int GetTextureIndexByName(string textureName)
        {
            for (int i = 0; i < _textureList.Count; i++)
            {
                if (_textureList[i].name.Contains(textureName))
                    return i;
            }

            return 0;
        }
    }
}