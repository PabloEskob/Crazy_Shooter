﻿// Copyright 2021, Infima Games. All Rights Reserved.

using System;
using System.Linq;
using Source.Infrastructure;
using Source.Scripts.Data;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
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

        private IStorage _storage;
        
        public override void Init()
        {
            _storage = AllServices.Container.Single<IStorage>();
            //Cache all weapons. Beware that weapons need to be parented to the object this component is on!
            weapons = GetComponentsInChildren<Weapon>(true);

            if(_storage!=null)
                foreach (var weapon in weapons)
                {
                    if (_storage.HasKeyString(weapon.GetName()))
                    {
                        weapon.SetData(_storage.GetString(weapon.GetName()));
                        weapon.SetBoolsFromData();
                    }
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
                Debug.Log($"weapon index {equippedWeaponIndex}");
            //Equip.
            //Equip(equippedAtStart);
            Equip(equippedWeaponIndex);
            
        }

        public override WeaponBehaviour Equip(int index)
        {
            Weapon current;
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
                
                if (_storage != null)
                {
                    current = equipped as Weapon;
                    _storage.SetString(current.GetName(), current.GetData().ToJson());
                }
                
                //equipped.GetComponent<Weapon>().Save(equipped as Weapon);
                equipped.gameObject.SetActive(false);
            }

            //Update index.
            equippedIndex = index;
            //Update equipped.
            equipped = weapons[equippedIndex];
            //Activate the newly-equipped weapon.
            equipped.gameObject.SetActive(true);
            equipped.SetEquipped();
            
            if (_storage != null)
            {
                current = equipped as Weapon;
                _storage.SetString(current.GetName(), current.GetData().ToJson());
                _storage.Save();
            }

            //equipped.GetComponent<Weapon>().Save(equipped as Weapon);
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