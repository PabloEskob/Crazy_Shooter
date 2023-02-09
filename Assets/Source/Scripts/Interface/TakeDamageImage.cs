using UnityEngine;
using DG.Tweening;

public class TakeDamageImage : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroupBackGround;

    private void Start() =>
        _canvasGroupBackGround.alpha = 0;

    public void ChangeAlpha()
    {
        if (_canvasGroupBackGround.alpha == 0)
            _canvasGroupBackGround.DOFade(1, 0.3f).SetLoops(2, LoopType.Yoyo);
    }
}