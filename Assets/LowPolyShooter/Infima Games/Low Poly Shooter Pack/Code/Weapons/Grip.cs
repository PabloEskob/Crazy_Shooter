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

        #endregion

        public override void SetIsBought()
        {
            throw new System.NotImplementedException();
        }

        public override void SetEquipped()
        {
            throw new System.NotImplementedException();
        }

        public override void SetUnequipped()
        {
            throw new System.NotImplementedException();
        }
    }
}