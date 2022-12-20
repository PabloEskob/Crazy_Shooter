using InfimaGames.LowPolyShooterPack;
using Source.Scripts.Data;
using UnityEngine;

namespace Source.Scripts.SimpleSaveSystem
{
    public static class SaveSystem
    {
        public static void WeaponSave(string weaponName, bool isBought, bool isEquipped)
        {
            PlayerPrefs.SetInt($"{weaponName}_isBought", DataExtensions.BoolToInt(isBought));
            PlayerPrefs.SetInt($"{weaponName}_isEquipped", DataExtensions.BoolToInt(isEquipped));
            PlayerPrefs.Save();
        }

        public static void Load(string weaponName, bool isBought, bool isEquipped)
        { 
            Debug.Log(weaponName);
            if(!isBought)
                isBought = DataExtensions.IntToBool(PlayerPrefs.GetInt($"{weaponName}_isBought"));
            
            isEquipped = DataExtensions.IntToBool(PlayerPrefs.GetInt($"{weaponName}_isEquipped"));
        }
    }
}