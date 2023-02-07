using InfimaGames.LowPolyShooterPack;
using UnityEngine;

namespace Assets.Source.Scripts.UI.Menus.Rewards
{
    [CreateAssetMenu(fileName = "NewWeaponReward", menuName = "Rewards/Weapon")]
    public class WeaponReward : Reward
    {
        [SerializeField] private Weapon _weapon;

        public override Sprite GetSprite() => _weapon.Icon;

        public override Weapon GetWeapon() => _weapon;
    }
}
