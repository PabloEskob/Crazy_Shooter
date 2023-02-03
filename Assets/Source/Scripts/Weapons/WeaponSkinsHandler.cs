using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Scripts.Weapons
{
    public class WeaponSkinsHandler : MonoBehaviour
    {
        [SerializeField] private Material _mainMaterial;
        [SerializeField] private List<Texture2D> _textureList;
        [SerializeField] private Texture2D _defaultTexture;

        private Texture2D _currentTexture;

        public Texture2D DefaultTexture => _defaultTexture;
        public IReadOnlyList<Texture2D> TextureList => _textureList;

        public void SetTexture(Texture2D texture)
        {
            _mainMaterial.mainTexture = texture;
        }

        private Texture2D GetTextureByIndex(int index) 
        {
            return _textureList[index];
        }
    }
}
