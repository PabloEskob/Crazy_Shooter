// Copyright 2021, Infima Games. All Rights Reserved.

using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    /// <summary>
    /// Grip.
    /// </summary>
    public class Grip : GripBehaviour
    {
        #region FIELDS SERIALIZED

        [Header("Settings")]

        [Tooltip("Sprite. Displayed on the player's interface.")]
        [SerializeField]
        private Sprite sprite;

        [SerializeField] private bool _isBought;
        [SerializeField] private bool _isEquipped;

        #endregion

        #region GETTERS

        public override Sprite GetSprite() => sprite;
        public override bool IsBought() => _isBought;

        public override bool IsEquipped() => _isEquipped;

        #endregion

        public override void SetIsBought() => _isBought = true;

        public override void SetEquipped() => _isEquipped = true;

        public override void SetUnequipped() => _isEquipped = false;
    }
}