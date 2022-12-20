// Copyright 2021, Infima Games. All Rights Reserved.

using System.Linq;
using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    public class Inventory : InventoryBehaviour
    {
        #region FIELDS
        
        /// <summary>
        /// Array of all weapons. These are gotten in the order that they are parented to this object.
        /// </summary>
        public Weapon[] weapons;
        
        /// <summary>
        /// Currently equipped WeaponBehaviour.
        /// </summary>
        private WeaponBehaviour equipped;
        /// <summary>
        /// Currently equipped index.
        /// </summary>
        private int equippedIndex = -1;

        #endregion
        
        #region METHODS

        
        
        public override void Init()
        {
            //Cache all weapons. Beware that weapons need to be parented to the object this component is on!
            weapons = GetComponentsInChildren<Weapon>(true);

            foreach (Weapon weapon in weapons)
            {
                weapon.Load();
            }
            
            var availableWeapons = weapons.Where(w => w.IsBought()).ToArray();
            
            //Disable all weapons. This makes it easier for us to only activate the one we need.
            foreach (Weapon weapon in availableWeapons)
                weapon.gameObject.SetActive(false);

            int equippedWeaponIndex = 0;

            for (int i = 0; i < availableWeapons.Length; i++)
            {
                if (availableWeapons[i].IsEquipped())
                    equippedWeaponIndex = i;
            }

            //Equip.
            //Equip(equippedAtStart);
            Equip(equippedWeaponIndex);
        }

        public override WeaponBehaviour Equip(int index)
        {
            //If we have no weapons, we can't really equip anything.
            if (weapons == null)
                return equipped;
            
            //The index needs to be within the array's bounds.
            if (index > weapons.Length - 1)
                return equipped;

            //No point in allowing equipping the already-equipped weapon.
            if (equippedIndex == index)
                return equipped;
            
            //Disable the currently equipped weapon, if we have one.
            if (equipped != null)
            {
                equipped.SetUnequipped();
                equipped.GetComponent<Weapon>().Save(equipped as Weapon);
                equipped.gameObject.SetActive(false);
            }

            //Update index.
            equippedIndex = index;
            //Update equipped.
            equipped = weapons[equippedIndex];
            //Activate the newly-equipped weapon.
            equipped.gameObject.SetActive(true);
            equipped.SetEquipped();
            equipped.GetComponent<Weapon>().Save(equipped as Weapon);
            //Return.
            return equipped;
        }
        
        #endregion

        #region Getters

        public override int GetLastIndex()
        {
            //Get last index with wrap around.
            int newIndex = equippedIndex - 1;
            if (newIndex < 0)
                newIndex = weapons.Length - 1;

            //Return.
            return newIndex;
        }

        public override int GetNextIndex()
        {
            //Get next index with wrap around.
            int newIndex = equippedIndex + 1;
            if (newIndex > weapons.Length - 1)
                newIndex = 0;

            //Return.
            return newIndex;
        }

        public override WeaponBehaviour GetEquipped() => equipped;
        public override int GetEquippedIndex() => equippedIndex;

        #endregion
    }
}