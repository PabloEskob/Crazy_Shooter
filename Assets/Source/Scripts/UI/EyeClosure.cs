using System;
using UnityEngine;

public class EyeClosure : MonoBehaviour
{
    private static readonly int Died = Animator.StringToHash("Died");

    private Animator _animator;
    private CutoutMaskUi _cutoutMaskUi;

    public event Action OnEndedAnimation;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _cutoutMaskUi = GetComponentInChildren<CutoutMaskUi>();
    }

    private void Start() => 
        _cutoutMaskUi.enabled = false;


    public void PlayScreen()
    {
        _cutoutMaskUi.enabled = true;
        _animator.SetBool(Died, true);
    }

    public void EndAnimation()
    {
        OnEndedAnimation?.Invoke();
    }
}