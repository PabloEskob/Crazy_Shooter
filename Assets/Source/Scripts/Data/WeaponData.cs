using System;
using InfimaGames.LowPolyShooterPack;

namespace Source.Scripts.Data
{
    [Serializable]
    public class WeaponData
    {
        public bool IsBought;
        public bool IsEquipped;
        
        
        public WeaponData(WeaponBehaviour weapon)
        {
            IsBought = weapon.IsBought();
            IsEquipped = weapon.IsEquipped();
        }
    }
}