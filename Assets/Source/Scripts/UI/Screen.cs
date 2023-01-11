using UnityEngine;

public class Screen : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    private Animation _animationComponent;
    private ButtonToMap _buttonToMap;
    
    public ButtonToMap ButtonToMap => _buttonToMap;

    private void Awake()
    {
        _animationComponent = GetComponent<Animation>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _buttonToMap = GetComponentInChildren<ButtonToMap>();
    }

    private void Start() =>
        _canvasGroup.alpha = 0;

    public void Show(AnimationClip animationClip)
    {
        _animationComponent.clip = animationClip;
        _animationComponent.Play();
    }
}