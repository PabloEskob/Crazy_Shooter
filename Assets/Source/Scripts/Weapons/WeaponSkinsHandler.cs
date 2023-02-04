using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Scripts.Weapons
{
    public class WeaponSkinsHandler : MonoBehaviour
    {
        [SerializeField] private Material _mainMaterial;
        [SerializeField] private List<Texture2D> _textureList;
        [SerializeField] private Texture2D _defaultTexture;

        public Texture2D CurrentTexture { get; private set; }

        public Texture2D DefaultTexture => _defaultTexture;
        public IReadOnlyList<Texture2D> TextureList => _textureList;

        public void SetTexture(Texture2D texture) => _mainMaterial.mainTexture = texture;

        public void ApplyTexture() => CurrentTexture = (Texture2D)_mainMaterial.mainTexture;

        public void ResetTexture()
        {
            if (CurrentTexture != null)
                SetTexture(CurrentTexture);
            else
                SetTexture(DefaultTexture);
        }

        private Texture2D GetTextureByIndex(int index) => _textureList[index];
    }
}
