using UnityEngine;

namespace Assets.Source.Scripts.UI.Menus.Rewards
{
    [CreateAssetMenu(fileName = "NewGrenadeReward", menuName = "Rewards/Grenade")]
    public class GrenadeReward : Reward
    {
        public override Sprite GetSprite() => Sprite;
    }
}
