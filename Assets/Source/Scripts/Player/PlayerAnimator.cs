using UnityEngine;
using DG.Tweening;

public class PlayerAnimator : MonoBehaviour
{
    private static readonly int Die = Animator.StringToHash("Die");
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Run = Animator.StringToHash("Run");

    private Animator _animator;
    private InfimaGames.LowPolyShooterPack.Movement _movement;
    private int _baseLayer;

    private void Awake()
    {
        _movement = GetComponent<InfimaGames.LowPolyShooterPack.Movement>();
        _animator = GetComponentInChildren<Animator>();
    }
    
    public void PlayWalking()
    {
        _movement.CanMove();
        _animator.SetBool(Run, true);
    }

    public void StopWalking()
    {
        _movement.NoMove();
        _animator.SetBool(Run, false);
    }

    public void PlayDeath()
    {
        var rotateToDeath = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, 90);
        transform.DORotate(rotateToDeath, 1.3f);
    }


    public void PlayHit()
    {
        Sequence sequence = DOTween.Sequence();
        
        if (gameObject.transform.eulerAngles.x == 0)
        {
            var rotateToHit = new Vector3(30, gameObject.transform.eulerAngles.y, gameObject.transform.eulerAngles.z);
            var initialRotate = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y,
                gameObject.transform.eulerAngles.z);
            sequence.Append(transform.DORotate(rotateToHit, 0.2f));
            sequence.Append(transform.DORotate(initialRotate, 0.2f));
        }
        
    }
}