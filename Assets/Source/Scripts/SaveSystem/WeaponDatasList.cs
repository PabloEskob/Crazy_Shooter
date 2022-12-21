using System;
using System.Collections.Generic;
using Source.Scripts.Data;
using UnityEngine;

namespace Source.Scripts.SaveSystem
{
    [Serializable]
    public static class WeaponDatasList
    {
        private static List<string> Datas = new List<string>();

        public static void AddNewData(string weaponData)
        {
            Datas.Add(weaponData);
            foreach (string data in Datas)
            {
                Debug.Log(data);
            }
        }

        public static bool Contains(string dataString)
        {
            foreach(string data in Datas)
                if (data.Contains(dataString))
                {
                    Debug.Log("Data Exist");
                    return true;
                }

            Debug.Log("Data Not Exist");
            return false;
        }
        
        
    }
}