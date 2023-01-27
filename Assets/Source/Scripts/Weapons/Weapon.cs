﻿// Copyright 2021, Infima Games. All Rights Reserved.

using System;
using Assets.Source.Scripts.Weapons;
using InfimaGames.LowPolyShooterPack.Legacy;
using Source.Scripts.Data;
using Source.Scripts.StaticData;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace InfimaGames.LowPolyShooterPack
{
    /// <summary>
    /// Weapon. This class handles most of the things that weapons need.
    /// </summary>
    public class Weapon : WeaponBehaviour
    {

        #region FIELDS SERIALIZED

        [SerializeField] private UpgradeConfig _upgradeConfig;
        [SerializeField] private BulletPool _bulletPool;

        [Header("Weapon stats")]
        [SerializeField] private float _damage;
        [SerializeField] private float _fireRate;
        [SerializeField] private float _reloadSpeed;
        [SerializeField] private float _magazineSize;
        [SerializeField] private int _maxUpgradeLevel = 4;
        [SerializeField] private bool _isInfinityAmmo;
        [SerializeField] private int Price = 100;
        [SerializeField] private Sprite _icon;
        
        [SerializeField] private WeaponTypes _weaponType;
        [SerializeField] private bool _isBought;
        [SerializeField] private bool _isEquipped;
        [SerializeField] private bool _isCollected;

        private int _frameUpgradeLevel;
        private int _muzzleUpgradeLevel;
        private int _scopeUpgradeLevel;
        private int _bulletsUpgradeLevel;
        private int _magazineSizeUpgradeLevel;

        public int MaxUpgradeLevel => _maxUpgradeLevel;
        public int WeaponPrice => Price;

        private const int FireRateDelta = 100;

        public event Action Bought;

        [Header("Settings")]

        [Tooltip("Weapon Name. Currently not used for anything, but in the future, we will use this for pickups!")]
        [SerializeField]
        private string weaponName;

        [Tooltip("How much the character's movement speed is multiplied by when wielding this weapon.")]
        [SerializeField]
        private float multiplierMovementSpeed = 1.0f;

        /// <summary>
        /// Attachment Manager.
        /// </summary>
        [SerializeField] private WeaponAttachmentManagerBehaviour attachmentManager;


        [Header("Firing")]

        [Tooltip("Is this weapon automatic? If yes, then holding down the firing button will continuously fire.")]
        [SerializeField]
        private bool automatic;

        [Tooltip("Is this weapon bolt-action? If yes, then a bolt-action animation will play after every shot.")]
        [SerializeField]
        private bool boltAction;

        [Tooltip("Amount of shots fired at once. Helpful for things like shotguns, where there are multiple projectiles fired at once.")]
        [SerializeField]
        private int shotCount = 1;

        [Tooltip("How far the weapon can fire from the center of the screen.")]
        [SerializeField]
        private float spread = 0.25f;

        [Tooltip("How fast the projectiles are.")]
        [SerializeField]
        private float projectileImpulse = 400.0f;

        [Tooltip("Amount of shots this weapon can shoot in a minute. It determines how fast the weapon shoots.")]
        [SerializeField]
        private float roundsPerMinutes = 200;

        [Tooltip("Mask of things recognized when firing.")]
        [SerializeField]
        private LayerMask mask;

        [Tooltip("Maximum distance at which this weapon can fire accurately. Shots beyond this distance will not use linetracing for accuracy.")]
        [SerializeField]
        private float maximumDistance = 500.0f;

        [Header("Reloading")]

        [Tooltip("Determines if this weapon reloads in cycles, meaning that it inserts one bullet at a time, or not.")]
        [SerializeField]
        private bool cycledReload;

        [Tooltip("Determines if the player can reload this weapon when it is full of ammunition.")]
        [SerializeField]
        private bool canReloadWhenFull = true;

        [Tooltip("Should this weapon be reloaded automatically after firing its last shot?")]
        [SerializeField]
        private bool automaticReloadOnEmpty;

        [Tooltip("Time after the last shot at which a reload will automatically start.")]
        [SerializeField]
        private float automaticReloadOnEmptyDelay = 0.25f;

        [Header("Animation")]

        [Tooltip("Transform that represents the weapon's ejection port, meaning the part of the weapon that casings shoot from.")]
        [SerializeField]
        private Transform socketEjection;

        [Tooltip("Weapon Bone Offsets.")]
        [SerializeField]
        private Offsets weaponOffsets;

        [Tooltip("Sway smoothing value. Makes the weapon sway smoother.")]
        [SerializeField]
        private float swaySmoothValue = 10.0f;

        [Tooltip("Character arms sway when wielding this weapon.")]
        [SerializeField]
        private Sway sway;

        [Header("Resources")]

        [Tooltip("Casing Prefab.")]
        [SerializeField]
        private GameObject prefabCasing;

        [Tooltip("Projectile Prefab. This is the prefab spawned when the weapon shoots.")]
        [SerializeField]
        private GameObject prefabProjectile;

        [Tooltip("The AnimatorController a player character needs to use while wielding this weapon.")]
        [SerializeField]
        public RuntimeAnimatorController controller;

        [Tooltip("Weapon Body Texture.")]
        [SerializeField]
        private Sprite spriteBody;

        [Header("Audio Clips Holster")]

        [Tooltip("Holster Audio Clip.")]
        [SerializeField]
        private AudioClip audioClipHolster;

        [Tooltip("Unholster Audio Clip.")]
        [SerializeField]
        private AudioClip audioClipUnholster;

        [Header("Audio Clips Reloads")]

        [Tooltip("Reload Audio Clip.")]
        [SerializeField]
        private AudioClip audioClipReload;

        [Tooltip("Reload Empty Audio Clip.")]
        [SerializeField]
        private AudioClip audioClipReloadEmpty;

        [Header("Audio Clips Reloads Cycled")]

        [Tooltip("Reload Open Audio Clip.")]
        [SerializeField]
        private AudioClip audioClipReloadOpen;

        [Tooltip("Reload Insert Audio Clip.")]
        [SerializeField]
        private AudioClip audioClipReloadInsert;

        [Tooltip("Reload Close Audio Clip.")]
        [SerializeField]
        private AudioClip audioClipReloadClose;

        [Header("Audio Clips Other")]

        [Tooltip("AudioClip played when this weapon is fired without any ammunition.")]
        [SerializeField]
        private AudioClip audioClipFireEmpty;

        [Tooltip("")]
        [SerializeField]
        private AudioClip audioClipBoltAction;

        #endregion

        #region FIELDS

        /// <summary>
        /// Weapon Animator.
        /// </summary>
        private Animator animator;

        /// <summary>
        /// Amount of ammunition left.
        /// </summary>
        private int ammunitionCurrent;

        #region Attachment Behaviours

        /// <summary>
        /// Equipped scope Reference.
        /// </summary>
        private ScopeBehaviour scopeBehaviour;

        /// <summary>
        /// Equipped Magazine Reference.
        /// </summary>
        private MagazineBehaviour magazineBehaviour;
        /// <summary>
        /// Equipped Muzzle Reference.
        /// </summary>
        private MuzzleBehaviour muzzleBehaviour;

        /// <summary>
        /// Equipped Laser Reference.
        /// </summary>
        private LaserBehaviour laserBehaviour;
        /// <summary>
        /// Equipped Grip Reference.
        /// </summary>
        private GripBehaviour gripBehaviour;

        #endregion

        /// <summary>
        /// The GameModeService used in this game!
        /// </summary>
        private IGameModeService gameModeService;
        /// <summary>
        /// The main player character behaviour component.
        /// </summary>
        private CharacterBehaviour characterBehaviour;

        /// <summary>
        /// The player character's camera.
        /// </summary>
        private Transform playerCamera;

        #endregion
        private WeaponData _data = new WeaponData();

        public WeaponUpgrade WeaponUpgrade { get; private set; }
        public UpgradeConfig UpgradeConfig => _upgradeConfig;

        public event Action WeaponInitialized;

        #region UNITY

        protected override void Awake()
        {
            //Get Animator.
            animator = GetComponent<Animator>();
            //Get Attachment Manager.
            attachmentManager = GetComponent<WeaponAttachmentManagerBehaviour>();

            //Cache the game mode service. We only need this right here, but we'll cache it in case we ever need it again.
            gameModeService = ServiceLocator.Current.Get<IGameModeService>();
            //Cache the player character.
            characterBehaviour = gameModeService.GetPlayerCharacter();
            //Cache the world camera. We use this in line traces.
            if (characterBehaviour != null)
                playerCamera = characterBehaviour.GetCameraWorld().transform;

            _data.WeaponName = weaponName;
            _data.IsBought = _isBought;
        }
        protected override void Start()
        {
            #region Cache Attachment References

            //Get Scope.
            scopeBehaviour = attachmentManager.GetEquippedScope();

            //Get Magazine.
            magazineBehaviour = attachmentManager.GetEquippedMagazine();
            //Get Muzzle.
            muzzleBehaviour = attachmentManager.GetEquippedMuzzle();

            //Get Laser.
            laserBehaviour = attachmentManager.GetEquippedLaser();
            //Get Grip.
            gripBehaviour = attachmentManager.GetEquippedGrip();

            #endregion


            //Max Out Ammo.
            magazineBehaviour.SetMagazineSize((int)_magazineSize);
            ammunitionCurrent = magazineBehaviour.GetMagazineSize();
            _fireRate = roundsPerMinutes / FireRateDelta;
            _data.FireRate = _fireRate;
            WeaponInitialized?.Invoke();
        }

        #endregion

        #region GETTERS

        public float Damage => _damage;
        public float FireRate => _fireRate;
        public float ReloadSpeed => _reloadSpeed;
        public float MagazineSize => _magazineSize;

        public override bool GetCollectedState() => _isCollected;

        public override string GetName() => weaponName;

        public override WeaponTypes GetWeaponType() => _weaponType;

        public WeaponData GetData() => _data;

        public Sprite GetIcon() => _icon;

        public void SetData(String name) =>
            _data = name.ToDeserialized<WeaponData>();

        public void SetBoolsFromData()
        {
            _isBought = _data.IsBought;
            _isEquipped = _data.IsEquipped;
        }

        public override Offsets GetWeaponOffsets() => weaponOffsets;

        public override float GetFieldOfViewMultiplierAim()
        {
            //Make sure we don't have any issues even with a broken setup!
            if (scopeBehaviour != null)
                return scopeBehaviour.GetFieldOfViewMultiplierAim();

            //Error.
            Debug.LogError("Weapon has no scope equipped!");

            //Return.
            return 1.0f;
        }
        public override float GetFieldOfViewMultiplierAimWeapon()
        {
            //Make sure we don't have any issues even with a broken setup!
            if (scopeBehaviour != null)
                return scopeBehaviour.GetFieldOfViewMultiplierAimWeapon();

            //Error.
            Debug.LogError("Weapon has no scope equipped!");

            //Return.
            return 1.0f;
        }

        public override Animator GetAnimator() => animator;

        public override float GetReloadSpeed() => _reloadSpeed;

        public override Sprite GetSpriteBody() => spriteBody;
        public override float GetMultiplierMovementSpeed() => multiplierMovementSpeed;

        public override AudioClip GetAudioClipHolster() => audioClipHolster;
        public override AudioClip GetAudioClipUnholster() => audioClipUnholster;

        public override AudioClip GetAudioClipReload() => audioClipReload;
        public override AudioClip GetAudioClipReloadEmpty() => audioClipReloadEmpty;

        public override AudioClip GetAudioClipReloadOpen() => audioClipReloadOpen;
        public override AudioClip GetAudioClipReloadInsert() => audioClipReloadInsert;
        public override AudioClip GetAudioClipReloadClose() => audioClipReloadClose;

        public override AudioClip GetAudioClipFireEmpty() => audioClipFireEmpty;
        public override AudioClip GetAudioClipBoltAction() => audioClipBoltAction;

        public override AudioClip GetAudioClipFire() => muzzleBehaviour.GetAudioClipFire();

        public override int GetAmmunitionCurrent() => ammunitionCurrent;

        public override int GetAmmunitionTotal() => magazineBehaviour.GetAmmunitionTotal();
        public override bool HasCycledReload() => cycledReload;

        public override bool IsBought() => _isBought;
        public override bool IsEquipped() => _isEquipped;

        public override bool IsAutomatic() => automatic;
        public override bool IsBoltAction() => boltAction;

        public override bool GetAutomaticallyReloadOnEmpty() => automaticReloadOnEmpty;
        public override float GetAutomaticallyReloadOnEmptyDelay() => automaticReloadOnEmptyDelay;

        public override bool CanReloadWhenFull() => canReloadWhenFull;
        public override float GetRateOfFire() => roundsPerMinutes;

        public override bool IsFull() => ammunitionCurrent == magazineBehaviour.GetMagazineSize();
        public override bool HasAmmunition() => ammunitionCurrent > 0;

        public override RuntimeAnimatorController GetAnimatorController() => controller;
        public override WeaponAttachmentManagerBehaviour GetAttachmentManager() => attachmentManager;

        public override Sway GetSway() => sway;
        public override float GetSwaySmoothValue() => swaySmoothValue;

        #endregion

        #region METHODS

        public override void SetIsBought()
        {
            _isBought = true;
            _data.IsBought = _isBought;
            Bought?.Invoke();
        }

        public override void SetEquipped()
        {
            _isEquipped = true;
            _data.IsEquipped = _isEquipped;
        }

        public override void SetUnequipped()
        {
            _isEquipped = false;
            _data.IsEquipped = _isEquipped;
        }

        public override void Reload()
        {
            animator.speed = _reloadSpeed;
            //Set Reloading Bool. This helps cycled reloads know when they need to stop cycling.
            const string boolName = "Reloading";
            animator.SetBool(boolName, true);

            //Play Reload Animation.
            animator.Play(cycledReload ? "Reload Open" : (HasAmmunition() ? "Reload" : "Reload Empty"), 0, 0.0f);
        }
        public override void Fire(float spreadMultiplier = 1.0f)
        {
            //We need a muzzle in order to fire this weapon!
            if (muzzleBehaviour == null)
                return;

            //Make sure that we have a camera cached, otherwise we don't really have the ability to perform traces.
            if (playerCamera == null)
                return;

            //Get Muzzle Socket. This is the point we fire from.
            Transform muzzleSocket = muzzleBehaviour.GetSocket();

            //Play the firing animation.
            const string stateName = "Fire";
            animator.Play(stateName, 0, 0.0f);
            //Reduce ammunition! We just shot, so we need to get rid of one!
            ammunitionCurrent = Mathf.Clamp(ammunitionCurrent - 1, 0, magazineBehaviour.GetMagazineSize());

            


            //Set the slide back if we just ran out of ammunition.
            if (ammunitionCurrent == 0)
                SetSlideBack(1);

            //Play all muzzle effects.
            //muzzleBehaviour.Effect();

            //Spawn as many projectiles as we need.
            for (var i = 0; i < shotCount; i++)
            {
                //Determine a random spread value using all of our multipliers.
                Vector3 spreadValue = Random.insideUnitSphere * (spread * spreadMultiplier);
                //Remove the forward spread component, since locally this would go inside the object we're shooting!
                spreadValue.z = 0;
                //Convert to world space.
                spreadValue = muzzleSocket.TransformDirection(spreadValue);

                //Determine the rotation that we want to shoot our projectile in.
                Quaternion rotation = Quaternion.LookRotation(playerCamera.forward * 1000.0f + spreadValue - muzzleSocket.position);

                //If there's something blocking, then we can aim directly at that thing, which will result in more accurate shooting.
                if (Physics.Raycast(new Ray(playerCamera.position, playerCamera.forward),
                    out RaycastHit hit, maximumDistance, mask))
                    rotation = Quaternion.LookRotation(hit.point + spreadValue - muzzleSocket.position);

                //Spawn projectile from the projectile spawn point.
                //GameObject projectile = Instantiate(prefabProjectile, muzzleSocket.position, rotation);
                Projectile projectile = _bulletPool.CreateProjectile();
                projectile.SetDamage(_damage);
                projectile.transform.position = muzzleSocket.position;
                projectile.transform.rotation = rotation;
                //Add velocity to the projectile.
                projectile.GetComponent<Rigidbody>().velocity = playerCamera.transform.forward * projectileImpulse;
                //projectile.transform.parent = null;
            }
        }

           private int ammunitionLeftInMagazine;
        public override void FillAmmunition(int amount)
        {
            ammunitionLeftInMagazine = ammunitionCurrent;
            //Update the value by a certain amount.
            ammunitionCurrent = amount != 0 ? Mathf.Clamp(ammunitionCurrent + amount,
                0, GetAvaliableAmmunition()) : GetAvaliableAmmunition();

            if (_isInfinityAmmo == false)
                magazineBehaviour.TryToDecreaseTotalAmmunition(magazineBehaviour.GetMagazineSize() - ammunitionLeftInMagazine);
        }

        private int GetAvaliableAmmunition()
        {
            int magazineSize = magazineBehaviour.GetMagazineSize();
            int ammunitionLeft = magazineBehaviour.GetAmmunitionTotal();
            Debug.Log(ammunitionCurrent);
            return ammunitionLeft > magazineSize ? magazineSize : ammunitionLeftInMagazine + ammunitionLeft;
        }

        public override void SetSlideBack(int back)
        {
            //Set the slide back bool.
            const string boolName = "Slide Back";
            animator.SetBool(boolName, back != 0);
        }

        public override void EjectCasing()
        {
            //Spawn casing prefab at spawn point.
            if (prefabCasing != null && socketEjection != null)
                Instantiate(prefabCasing, socketEjection.position, socketEjection.rotation);
        }

        public void Upgrade(float damage, float fireRate, float reloadSpeed, float magazineSize)
        {
            _damage += damage;
            _fireRate += fireRate;
            _reloadSpeed += reloadSpeed;
            _magazineSize += magazineSize;

            switch (WeaponUpgrade)
            {
                case GunFrameUpgrade:
                    _frameUpgradeLevel++;
                    break;
                case Source.Scripts.StaticData.MuzzleUpgrade:
                    _muzzleUpgradeLevel++;
                    break;
                case BulletUpgrade:
                    _bulletsUpgradeLevel++;
                    break;
                case Source.Scripts.StaticData.ScopeUpgrade:
                    _scopeUpgradeLevel++;
                    break;
                case Source.Scripts.StaticData.MagazineUpgrade:
                    _magazineSizeUpgradeLevel++;
                    break;
            }
        }


        public WeaponUpgrade GetFrameUpgrade()
        {
            for (int i = 0; i < UpgradeConfig.GunFrameUpgrades.Count; i++)
            {
                int level = UpgradeConfig.GunFrameUpgrades[i].Level;

                if (level - 1 == _frameUpgradeLevel)
                {
                    WeaponUpgrade = UpgradeConfig.GunFrameUpgrades[i];
                    return WeaponUpgrade;
                }
            }
            return null;
        }

        public WeaponUpgrade GetMuzzleUpgrade()
        {
            for (int i = 0; i < UpgradeConfig.MuzzleUpgrades.Count; i++)
            {
                int level = UpgradeConfig.MuzzleUpgrades[i].Level;

                if (level - 1 == _muzzleUpgradeLevel)
                {
                    WeaponUpgrade = UpgradeConfig.MuzzleUpgrades[i];
                    return WeaponUpgrade;
                }
            }
            return null;
        }

        public WeaponUpgrade GetScopeUpgrade()
        {
            for (int i = 0; i < UpgradeConfig.ScopeUpgrades.Count; i++)
            {
                int level = UpgradeConfig.ScopeUpgrades[i].Level;

                if (level - 1 == _scopeUpgradeLevel)
                {
                    WeaponUpgrade = UpgradeConfig.ScopeUpgrades[i];
                    return WeaponUpgrade;
                }
            }
            return null;
        }

        public WeaponUpgrade GetBulletsUpgrade()
        {
            for (int i = 0; i < UpgradeConfig.BulletUpgrades.Count; i++)
            {
                int level = UpgradeConfig.BulletUpgrades[i].Level;

                if (level - 1 == _bulletsUpgradeLevel)
                {
                    WeaponUpgrade = UpgradeConfig.BulletUpgrades[i];
                    return WeaponUpgrade;
                }
            }
            return null;
        }

        public WeaponUpgrade GetMagazineUpgrade()
        {
            for (int i = 0; i < UpgradeConfig.MagazineUpgrades.Count; i++)
            {
                int level = UpgradeConfig.MagazineUpgrades[i].Level;

                if (level - 1 == _magazineSizeUpgradeLevel)
                {
                    WeaponUpgrade = UpgradeConfig.MagazineUpgrades[i];
                    return WeaponUpgrade;
                }
            }
            return null;
        }

        public void UpdateStatsToData()
        {
            _data.Damage = _damage;
            _data.FireRate = _fireRate;
            _data.Reload = _reloadSpeed;
            _data.MagazineSize = _magazineSize;
            _data.FrameUpgradeLevel = _frameUpgradeLevel;
            _data.MuzzleUpgradeLevel = _muzzleUpgradeLevel;
            _data.ScopeUpgradeLevel = _scopeUpgradeLevel;
            _data.BulletsUpgradeLevel = _bulletsUpgradeLevel;
            _data.MagazineUpgradeLevel = _magazineSizeUpgradeLevel;
        }

        public void UpdateStatsFromData()
        {
            _damage = _data.Damage;
            _fireRate = _data.FireRate;
            roundsPerMinutes = _fireRate * FireRateDelta;
            _reloadSpeed = _data.Reload;
            _magazineSize = _data.MagazineSize;
            _frameUpgradeLevel = _data.FrameUpgradeLevel;
            _muzzleUpgradeLevel = _data.MuzzleUpgradeLevel;
            _scopeUpgradeLevel = _data.ScopeUpgradeLevel;
            _bulletsUpgradeLevel = _data.BulletsUpgradeLevel;
            _magazineSizeUpgradeLevel = _data.MagazineUpgradeLevel;
        }

        #endregion
    }
}