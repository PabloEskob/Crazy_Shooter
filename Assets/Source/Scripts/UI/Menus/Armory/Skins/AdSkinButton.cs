using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Scripts.UI.Menus.Armory.Skins
{
    public class AdSkinButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private const float ScaleTime = 0f;
        private const float MaxScale = 1f;
        private const float MinScale = 0f;

        public Button AdSkinUIButton => _button;

        public void ChangeScale(bool value)
        {
            if (!value)
                transform.DOScale(MinScale, ScaleTime);
            else
                transform.DOScale(MaxScale, ScaleTime);
        }
    }
}
