﻿using System;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimator))]
public class EnemyHealth : MonoBehaviour, IHealth
{
    [SerializeField] private EnemyAnimator _enemyAnimator;
    [SerializeField] private HeadShot _headShot;
    [SerializeField] private BodyShot _bodyShot;
    [SerializeField] private int _damageMultiplier = 6;

    private Effects _effects;
    private EnemyMove _enemyMove;
    private bool _canPlayHit = true;

    public Effects Effects => _effects;
    public float Current { get; set; }
    public float Max { get; set; }
    public bool CanHit { get; set; }

    public event Action HealthChanged;

    private void OnEnable()
    {
        _headShot.Hitted += HeadShot;
        _bodyShot.Hitted += TakeHitBody;
    }

    private void OnDisable()
    {
        _headShot.Hitted -= HeadShot;
        _bodyShot.Hitted -= TakeHitBody;
    }

    private void Start()
    {
        _effects = GetComponent<Effects>();
        _enemyMove = GetComponent<EnemyMove>();
    }

    public void OnHitEnded()
    {
        _canPlayHit = true;
        _enemyMove.ContinueMove();
    }

    public void OnStartHit()
    {
        _canPlayHit = false;
        _enemyMove.StopMove();
    }

    public void TakeDamage(int damage)
    {
        if (CanHit)
        {
            Max -= damage;

            if (_canPlayHit)
                _enemyAnimator.PlayHit();

            HealthChanged?.Invoke();
        }
        
    }

    private void TakeHitBody(int damage, Collision collision)
    {
        _effects.GetContactCollision(collision);
        TakeDamage(damage);
    }

    private void HeadShot(int damage, Collision collision)
    {
        TakeDamage(damage * _damageMultiplier);

        if (Max > 0)
            _effects.GetContactCollision(collision);
        else
        {
            _effects.PlayHeadShot();
            _headShot.DestroyHead();
        }
    }
}