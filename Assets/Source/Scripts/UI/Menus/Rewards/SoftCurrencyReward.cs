using UnityEngine;

namespace Assets.Source.Scripts.UI.Menus.Rewards
{

    [CreateAssetMenu(fileName = "NewSoftCurrencyReward", menuName = "Rewards/Soft")]
    public class SoftCurrencyReward : Reward
    {
        public override Sprite GetSprite() => Sprite;
    }
}
