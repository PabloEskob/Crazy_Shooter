using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieSounds : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _audioClips;
    [SerializeField] private int _maxDelay;
    [SerializeField] private int _minDelay;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public IEnumerator PlaySound()
    {
        var seconds = new WaitForSeconds(Random.Range(_minDelay,_maxDelay));
        
        while (true)
        {
            yield return seconds;
            ChooseSound();
        }
    }

    private void ChooseSound()
    {
        int value = Random.Range(0, _audioClips.Count);
        _audioSource.clip = _audioClips[value];
        _audioSource.Play();
    }
}