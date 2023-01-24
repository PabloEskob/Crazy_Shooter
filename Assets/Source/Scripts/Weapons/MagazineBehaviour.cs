﻿// Copyright 2021, Infima Games. All Rights Reserved.

using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    /// <summary>
    /// Magazine Behaviour.
    /// </summary>
    public abstract class MagazineBehaviour : MonoBehaviour
    {
        #region GETTERS
        
        /// <summary>
        /// Returns The Total Ammunition.
        /// </summary>
        public abstract int GetAmmunitionTotal();
        /// <summary>
        /// Returns the Sprite used on the Character's Interface.
        /// </summary>
        public abstract Sprite GetSprite();

        public abstract int GetMagazineSize();

        public abstract void SetMagazineSize(int amount);

        public abstract void TryToDecreaseTotalAmmunition(int amount);

        #endregion
    }
}