using UnityEngine;

namespace Assets.Source.Scripts.Weapons
{
    public class WeaponSkinsHandler : MonoBehaviour
    {
        [SerializeField] private Material _mainMaterial;

        private Texture2D _currentTexture;

        private void SetTexture()
        {
            _mainMaterial.mainTexture = _currentTexture;
        }
    }
}
