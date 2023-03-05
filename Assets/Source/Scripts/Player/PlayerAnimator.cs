using UnityEngine;
using DG.Tweening;

public class PlayerAnimator : MonoBehaviour
{
    private static readonly int Die = Animator.StringToHash("Die");
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Run = Animator.StringToHash("Run");

    [Range(0, 30)] [SerializeField] private int _tiltOnImpact = 20;

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
        var eulerAngles = gameObject.transform.eulerAngles;
        var rotateToDeath = new Vector3(-80, eulerAngles.y, eulerAngles.z);
        transform.DORotate(rotateToDeath, 1.3f);
        
        if (Camera.main != null)
        {
            var cameraRotate = new Vector3(60, 0 ,0);
            Camera.main.transform.DOLocalRotate(cameraRotate, 1.5f);
        }
    }


    public void PlayHit()
    {
        Sequence sequence = DOTween.Sequence();

        if (gameObject.transform.eulerAngles.x == 0)
        {
            var eulerAngles = gameObject.transform.eulerAngles;
            var rotateToHit = new Vector3(_tiltOnImpact, eulerAngles.y, eulerAngles.z);
            var initialRotate = new Vector3(eulerAngles.x, eulerAngles.y,
                eulerAngles.z);
            sequence.Append(transform.DORotate(rotateToHit, 0.2f));
            sequence.Append(transform.DORotate(initialRotate, 0.2f));
        }
    }
}