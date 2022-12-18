using System;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour, IAnimationStateReader
{
    private static readonly int Die = Animator.StringToHash("Die");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int IsMoving = Animator.StringToHash("Move");
    private static readonly int Hit = Animator.StringToHash("Hit");

    private static readonly int _deathStateHash = Animator.StringToHash("die");
    private static readonly int _attackStateHash = Animator.StringToHash("attack");
    private static readonly int _walkingStateHash = Animator.StringToHash("move");

    private Animator _animator;

    public AnimatorState State { get; private set; }

    public event Action<AnimatorState> StateEntered;
    public event Action<AnimatorState> StateExited;

    private void Awake() =>
        _animator = GetComponent<Animator>();

    public void PlayHit() =>
        _animator.SetTrigger(Hit);

    public void PlayDeath() =>
        _animator.SetTrigger(Die);

    public void PlayAttack() =>
        _animator.SetTrigger(Attack);

    public void Move() =>
        _animator.SetBool(IsMoving, true);

    public void StopMove() =>
        _animator.SetBool(IsMoving, false);

    public void EnteredState(int stateHash)
    {
        State = StateFor(stateHash);
        StateEntered?.Invoke(State);
    }

    public void ExitedState(int stateHash) =>
        StateExited?.Invoke(StateFor(stateHash));

    private AnimatorState StateFor(int stateHash)
    {
        AnimatorState state;

        if (stateHash == _attackStateHash)
            state = AnimatorState.Attack;
        else if (stateHash == _deathStateHash)
            state = AnimatorState.Died;
        else if (stateHash == _walkingStateHash)
            state = AnimatorState.Idle;
        else
            state = AnimatorState.Unknown;

        return state;
    }
}