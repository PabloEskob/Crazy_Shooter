using System.Collections.Generic;
using Assets.Source.Scripts.UI;
using Source.Infrastructure;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class ZombieSounds : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _audioClips;

    private AudioSource _audioSource;
    private IStorage _storage;
    
    private void OnDisable() => 
        _storage.Changed -= Change;

    private void Awake() => 
        _audioSource = GetComponent<AudioSource>();

    private void Start()
    {
        _storage = AllServices.Container.Single<IStorage>();
        _storage.Changed += Change;
        Change();
    }

    private void Change()
    {
        if (_storage.HasKeyFloat(SettingsNames.SoundSettingsKey))
            _audioSource.volume = _storage.GetFloat(SettingsNames.SoundSettingsKey);
    }
    
    protected void ChooseSound()
    {
        int value = Random.Range(0, _audioClips.Count);
        _audioSource.clip = _audioClips[value];
        _audioSource.Play();
    }
}