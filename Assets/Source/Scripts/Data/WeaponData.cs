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
        public int Level;
        public float Damage;
        public float FireRate;
        public float Reload;
        public float MagazineSize;
        public int FrameUpgradeLevel;
        public int MuzzleUpgradeLevel;
        public int ScopeUpgradeLevel;
        public int BulletsUpgradeLevel;
        public int MagazineUpgradeLevel;
    }
}