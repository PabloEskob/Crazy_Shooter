using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private static readonly int Die = Animator.StringToHash("Die");
    private static readonly int Hit = Animator.StringToHash("Hit");

    private Animator _animator;

    private void Awake() => 
        _animator = GetComponentInChildren<Animator>();

    public void PlayDeath()
    {
        Debug.Log("Смерть игрока!");
    }


    public void PlayHit()
    {
        Debug.Log("Удар по игроку!");
    }
        
}