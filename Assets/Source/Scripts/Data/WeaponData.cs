using System;
using InfimaGames.LowPolyShooterPack;

namespace Source.Scripts.Data
{
    [Serializable]
    public class WeaponData
    {
        public string WeaponName;
        public bool IsBought;
        public bool IsEquipped;
        public string SaveTime = DateTime.MinValue.ToString();
        
        
    }
}