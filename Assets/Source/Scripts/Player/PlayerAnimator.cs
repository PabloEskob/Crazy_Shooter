﻿using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private static readonly int Die = Animator.StringToHash("Die");
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Run = Animator.StringToHash("Run");

    private Animator _animator;

    private void Awake() => 
        _animator = GetComponentInChildren<Animator>();

    public void PlayWalking() => 
        _animator.SetBool(Run,true);

    public void StopWalking() => 
        _animator.SetBool(Run,false);

    public void PlayDeath()
    {
        Debug.Log("Смерть игрока!");
    }


    public void PlayHit()
    {
        Debug.Log("Удар по игроку!");
    }
        
}