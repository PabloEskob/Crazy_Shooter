using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieSounds : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _audioClips;
    [SerializeField] private int _delay;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public IEnumerator PlaySound()
    {
        var seconds = new WaitForSeconds(_delay);
        
        while (true)
        {
            ChooseSound();
            yield return seconds;
        }
    }

    private void ChooseSound()
    {
        int value = Random.Range(0, _audioClips.Count);
        _audioSource.clip = _audioClips[value];
        _audioSource.Play();
    }
}