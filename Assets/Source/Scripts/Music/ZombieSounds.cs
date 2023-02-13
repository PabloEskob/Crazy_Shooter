using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class ZombieSounds : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _audioClips;

    private AudioSource _audioSource;

    private void Awake() => 
        _audioSource = GetComponent<AudioSource>();


    protected void ChooseSound()
    {
        int value = Random.Range(0, _audioClips.Count);
        _audioSource.clip = _audioClips[value];
        _audioSource.Play();
    }
}