using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Scripts.StaticData
{
    [CreateAssetMenu(fileName = "NewUpgradeConfig", menuName = "StaticData/UpgradeConfig")]
    public class UpgradeConfig : ScriptableObject
    {
        [SerializeField] private WeaponStats[] stats;
    }

    [Serializable]
    public class WeaponStats
    {
        [SerializeField] private int _level;
        [SerializeField] private int _damage;
        [SerializeField] private int _fireRate;
        [SerializeField] private int _reload;
        [SerializeField] private int _magazineSize;
        [SerializeField] private int _price;

        public int Level => _level;
        public int Damage => _damage;
        public int FireRate => _fireRate;
        public int Reload => _reload;
        public int MagazineSize => _magazineSize;
        public int Price => _price;
    }
}
