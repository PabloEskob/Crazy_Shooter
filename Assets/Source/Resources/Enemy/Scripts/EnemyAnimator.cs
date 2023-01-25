﻿using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private static readonly int Die = Animator.StringToHash("Die");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int IsMoving = Animator.StringToHash("Move");
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Speed = Animator.StringToHash("Speed");

    private Animator _animator;
    private string _clipName;
    private Animation _animation;

    private void Awake() =>
        _animator = GetComponent<Animator>();

    public void PlayHit() =>
        _animator.SetTrigger(Hit);

    public void PlayIdle() =>
        _animator.SetTrigger(Idle);

    public void PlayDeath() =>
        _animator.SetTrigger(Die);

    public void PlayAttack() =>
        _animator.SetTrigger(Attack);

    public void Move(float speed)
    {
        _animator.SetBool(IsMoving, true);
        _animator.SetFloat(Speed, 1 + speed / 10);
    }

    public void StopMove() =>
        _animator.SetBool(IsMoving, false);
}