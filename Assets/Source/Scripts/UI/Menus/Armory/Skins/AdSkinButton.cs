using Assets.Source.Scripts.UI.Menus.Rewards;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Scripts.UI.Menus.Armory.Skins
{
    public class AdSkinButton : AdvertisementButton
    {
        private const float ScaleTime = 0f;
        private const float MaxScale = 1f;
        private const float MinScale = 0f;

        public override void ChangeScale(bool value)
        {
            if (!value)
                transform.DOScale(MinScale, ScaleTime);
            else
                transform.DOScale(MaxScale, ScaleTime);
        }
    }
}
