using System;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Scripts.StaticData
{
    [CreateAssetMenu(fileName = "NewUpgradeConfig", menuName = "StaticData/UpgradeConfig")]
    public class UpgradeConfig : ScriptableObject
    {
        [SerializeField] private List<GunFrameUpgrade> _gunFrameUpgrades;
        [SerializeField] private List<MuzzleUpgrade> _muzzleUpgrades;
        [SerializeField] private List<ScopeUpgrade> _scopeUpgrades;
        [SerializeField] private List<BulletUpgrade> _bulletsUpgrades;
        [SerializeField] private List<MagazineUpgrade> _magazineUpgrades;

        public List<GunFrameUpgrade> GunFrameUpgrades => _gunFrameUpgrades;
        public List<MuzzleUpgrade> MuzzleUpgrades => _muzzleUpgrades;
        public List<ScopeUpgrade> ScopeUpgrades => _scopeUpgrades;
        public List<BulletUpgrade> BulletUpgrades => _bulletsUpgrades;
        public List<MagazineUpgrade> MagazineUpgrades => _magazineUpgrades;
    }

    [Serializable]
    public class GunFrameUpgrade : WeaponUpgrade
    {
       
        public override void GetName()
        {
            throw new NotImplementedException();
        }
    }

    [Serializable]
    public class MuzzleUpgrade : WeaponUpgrade
    {
        public override void GetName()
        {
            throw new NotImplementedException();
        }
    }
    
    [Serializable]
    public class ScopeUpgrade : WeaponUpgrade
    {
        public override void GetName()
        {
            throw new NotImplementedException();
        }
    }
    
    [Serializable]
    public class BulletUpgrade : WeaponUpgrade
    {
        public override void GetName()
        {
            throw new NotImplementedException();
        }
    }
    
    [Serializable]
    public class MagazineUpgrade : WeaponUpgrade
    {
        public override void GetName()
        {
            throw new NotImplementedException();
        }
    }
    
    public abstract class WeaponUpgrade
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
