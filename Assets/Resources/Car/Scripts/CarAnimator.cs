using UnityEngine;

public class CarAnimator : MonoBehaviour
{
    private static readonly int Die = Animator.StringToHash("Die");
    private static readonly int Hit = Animator.StringToHash("Hit");

    private Animator _animator;

    private void Awake() =>
        _animator = GetComponent<Animator>();

    public void PlayDeath() =>
        _animator.SetTrigger(Die);

    public void PlayHit() =>
        _animator.SetTrigger(Hit);
}