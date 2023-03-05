using System;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Scripts.StaticData
{
    [CreateAssetMenu(fileName = "NewUpgradeConfig", menuName = "StaticData/UpgradeConfig")]
    public class UpgradeConfig : ScriptableObject
    {
        [SerializeField] private List<GunFrameUpgradeData> _gunFrameUpgrades;
        [SerializeField] private List<MuzzleUpgradeData> _muzzleUpgrades;
        [SerializeField] private List<ScopeUpgradeData> _scopeUpgrades;
        [SerializeField] private List<BulletUpgradeData> _bulletsUpgrades;
        [SerializeField] private List<MagazineUpgradeData> _magazineUpgrades;

        public List<GunFrameUpgradeData> GunFrameUpgrades => _gunFrameUpgrades;
        public List<MuzzleUpgradeData> MuzzleUpgrades => _muzzleUpgrades;
        public List<ScopeUpgradeData> ScopeUpgrades => _scopeUpgrades;
        public List<BulletUpgradeData> BulletUpgrades => _bulletsUpgrades;
        public List<MagazineUpgradeData> MagazineUpgrades => _magazineUpgrades;
    }

    [Serializable]
    public class GunFrameUpgradeData : WeaponUpgradeData
    {
       
        public override void GetName()
        {
            throw new NotImplementedException();
        }
    }

    [Serializable]
    public class MuzzleUpgradeData : WeaponUpgradeData
    {
        public override void GetName()
        {
            throw new NotImplementedException();
        }
    }
    
    [Serializable]
    public class ScopeUpgradeData : WeaponUpgradeData
    {
        public override void GetName()
        {
            throw new NotImplementedException();
        }
    }
    
    [Serializable]
    public class BulletUpgradeData : WeaponUpgradeData
    {
        public override void GetName()
        {
            throw new NotImplementedException();
        }
    }
    
    [Serializable]
    public class MagazineUpgradeData : WeaponUpgradeData
    {
        public override void GetName()
        {
            throw new NotImplementedException();
        }
    }
    
    public abstract class WeaponUpgradeData
    {
        [SerializeField] protected int _level;
        [SerializeField] protected float _damage;
        [SerializeField] protected float _fireRate;
        [SerializeField] protected float _reload;
        [SerializeField] protected float _magazineSize;
        [SerializeField] protected int _price;

        public int Level => _level;
        public float Damage => _damage;
        public float FireRate => _fireRate;
        public float Reload => _reload;
        public float MagazineSize => _magazineSize;
        public int Price => _price;

        public abstract void GetName();
    }
}
