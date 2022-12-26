using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Source.Scripts.StaticData
{
    [CreateAssetMenu(fileName = "NewUpgradeConfig", menuName = "StaticData/UpgradeConfig")]
    public class UpgradeConfig : ScriptableObject
    {
        [SerializeField] private GunFrameUpgrade[] _gunFrameUpgrades;
        [SerializeField] private MuzzleUpgrade[] _muzzleUpgrades;
        [SerializeField] private ScopeUpgrade[] _scopeUpgrades;
        [SerializeField] private BulletUpgrade[] _bulletsUpgrades;
        [SerializeField] private MagazineUpgrade[] _magazineUpgrades;

        public IReadOnlyCollection<GunFrameUpgrade> GunFrameUpgrades => _gunFrameUpgrades;
        public IReadOnlyCollection<MuzzleUpgrade> MuzzleUpgrades => _muzzleUpgrades;
        public IReadOnlyCollection<ScopeUpgrade> ScopeUpgrades => _scopeUpgrades;
        public IReadOnlyCollection<BulletUpgrade> BulletUpgrades => _bulletsUpgrades;
        public IReadOnlyCollection<MagazineUpgrade> MagazineUpgrades => _magazineUpgrades;
    }

    [Serializable]
    public class GunFrameUpgrade : WeaponUpgrade
    {
        public int Level => _level;
        public int Damage => _damage;
        public int FireRate => _fireRate;
        public int Reload => _reload;
        public int MagazineSize => _magazineSize;
        public int Price => _price;
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
        [SerializeField] protected int _damage;
        [SerializeField] protected int _fireRate;
        [SerializeField] protected int _reload;
        [SerializeField] protected int _magazineSize;
        [SerializeField] protected int _price;

        public abstract void GetName();
    }
}
