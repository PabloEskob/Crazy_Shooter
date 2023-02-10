using System;
using Assets.Source.Scripts.UI;
using Source.Infrastructure;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

public class MusicSettings : MonoBehaviour
{
    private IStorage _storage;
    private AudioSource _audioSource;

    private void OnDisable() => 
        _storage.Changed -= Change;

    private void Awake() =>
        _audioSource = GetComponent<AudioSource>();

    private void Start()
    {
        _storage = AllServices.Container.Single<IStorage>();
        _storage.Changed += Change;
        Change();
        _audioSource.Play();
    }

    private void Change()
    {
        if (_storage.HasKeyFloat(SettingsNames.SoundSettingsKey))
            _audioSource.volume = _storage.GetFloat(SettingsNames.MusicSettingsKey);
    }
    
}