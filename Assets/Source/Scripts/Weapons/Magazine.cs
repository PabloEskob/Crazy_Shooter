// Copyright 2021, Infima Games. All Rights Reserved.

using Source.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    /// <summary>
    /// Magazine.
    /// </summary>
    public class Magazine : MagazineBehaviour
    {
        #region FIELDS SERIALIZED

        [Header("Settings")]

        [Tooltip("Total Ammunition.")]
        [SerializeField]
        private int ammunitionTotal = 10;

        [Header("Interface")]

        [Tooltip("Interface Sprite.")]
        [SerializeField]
        private Sprite sprite;

        [SerializeField] private int _magazineSize;


        #endregion

        #region GETTERS

        /// <summary>
        /// Ammunition Total.
        /// </summary>
        public override int GetAmmunitionTotal() => ammunitionTotal;

        public override int GetMagazineSize() => _magazineSize;

        /// <summary>
        /// Sprite.
        /// </summary>
        public override Sprite GetSprite() => sprite;

        public override void SetMagazineSize(int amount) =>
            _magazineSize = amount;

        public override void TryToDecreaseTotalAmmunition(int amount)
        {
            if (ammunitionTotal > 0)
                ammunitionTotal -= amount;
        }

        #endregion
    }
}