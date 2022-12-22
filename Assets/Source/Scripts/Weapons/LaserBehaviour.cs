﻿// Copyright 2021, Infima Games. All Rights Reserved.

using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    /// <summary>
    /// Laser Abstract Class.
    /// </summary>
    public abstract class LaserBehaviour : MonoBehaviour
    {
        #region GETTERS

        /// <summary>
        /// Returns the Sprite used on the Character's Interface.
        /// </summary>
        public abstract Sprite GetSprite();
        public abstract bool IsBought();
        public abstract bool IsEquipped();

        #endregion
        
        
        public abstract void SetIsBought();
        public abstract void SetEquipped();
        public abstract void SetUnequipped();
    }
}