using System;
using Assets.Source.Scripts.UI;
using Source.Infrastructure;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
using Source.Scripts.Music;
using UnityEngine;

public class Effects : MonoBehaviour
{
    [SerializeField] private ParticleSystem _fxHeadShot;
    [SerializeField] private ParticleSystem _deathFx;
    [SerializeField] private ParticleSystem _blood;
    [SerializeField] private AudioSource _headShot;

    private SoundDiedZombie _zombieDied;
    private IStorage _storage;
    private BloodEffectSpawner _bloodEffectSpawner;
    private AudioSource _audioSourceZombieDied;

    private void Awake()
    {
        _zombieDied = GetComponentInChildren<SoundDiedZombie>();
        _bloodEffectSpawner = GetComponentInChildren<BloodEffectSpawner>();
        _audioSourceZombieDied = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _storage = AllServices.Container.Single<IStorage>();

        if (_storage.HasKeyFloat(SettingsNames.SoundSettingsKey))
        {
            _headShot.volume = _storage.GetFloat(SettingsNames.SoundSettingsKey);
            _audioSourceZombieDied.volume = _storage.GetFloat(SettingsNames.SoundSettingsKey);
        }
    }

    public void GetContactCollision(Collision collision) =>
        _bloodEffectSpawner.Init(collision.GetContact(0).point);

    public void PlayHeadShot()
    {
        _fxHeadShot.Play();
        //_headShot.Play();
        _blood.Play();
    }

    public void PlayDeath()
    {
       // _zombieDied.PlaySound();
        _deathFx.Play();
    }
}