using Assets.Source.Scripts.UI;
using Source.Infrastructure;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

public class Effects : MonoBehaviour
{
    [SerializeField] private BloodEffectSpawner _bloodEffectSpawner;
    [SerializeField] private ParticleSystem _fxHeadShot;
    [SerializeField] private ParticleSystem _deathFx;
    [SerializeField] private ParticleSystem _blood;
    [SerializeField] private AudioSource _headShot;

    private IStorage _storage;

    private void Start()
    {
        _storage = AllServices.Container.Single<IStorage>();

        if (_storage.HasKeyInt(SettingsNames.SoundSettingsKey))
            _headShot.volume = _storage.GetInt(SettingsNames.SoundSettingsKey);
    }

    public void GetContactCollision(Collision collision) =>
        _bloodEffectSpawner.Init(collision.GetContact(0).point);

    public void PlayHeadShot()
    {
        _fxHeadShot.Play();
        _headShot.Play();
        _blood.Play();
    }

    public void PlayDeath() =>
        _deathFx.Play();
}