using System;

namespace Source.Scripts.Data
{
    [Serializable]
    public class WeaponProgress
    {
        private readonly bool _isBought;
        private readonly bool _isEquipped;

        public WeaponProgress(bool isBought, bool isEquipped)
        {
            _isBought = isBought;
            _isEquipped = isEquipped;
        }
    }
}