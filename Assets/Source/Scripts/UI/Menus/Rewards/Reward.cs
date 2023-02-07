using InfimaGames.LowPolyShooterPack;
using UnityEngine;

namespace Assets.Source.Scripts.UI.Menus.Rewards
{
    public abstract class Reward : ScriptableObject
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private int _quantity;
        [SerializeField] private RewardType _type;

        public Sprite Sprite => _sprite;
        public int Quantity => _quantity;
        public RewardType Type => _type;

        public abstract Sprite GetSprite();
        public virtual Weapon GetWeapon() { return null; }
    }

    public enum RewardType
    {
        Weapon,
        SoftCurrency,
        Grenade
    }
}