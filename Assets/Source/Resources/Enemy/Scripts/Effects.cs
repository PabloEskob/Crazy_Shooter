﻿using UnityEngine;

public class Effects : MonoBehaviour
{
    [SerializeField] private BloodEffectSpawner _bloodEffectSpawner;
    [SerializeField] private ParticleSystem _fxHeadShot;
    [SerializeField] private ParticleSystem _deathFx;
    [SerializeField] private ParticleSystem _blood;
    [SerializeField] private AudioSource _headShot;

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