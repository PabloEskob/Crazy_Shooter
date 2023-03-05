using UnityEngine;

public class Screen : MonoBehaviour
{
    [SerializeField] private ButtonToMap _buttonToMap;
    [SerializeField] private CanvasGroup _canvasGroup;

    private Animation _animationComponent;

    public ButtonToMap ButtonToMap => _buttonToMap;

    private void Awake() => 
        _animationComponent = GetComponent<Animation>();

    protected virtual void Start() => _canvasGroup.alpha = 0;

    public void Show(AnimationClip animationClip)
    {
        Debug.Log("ShowDeft");
        _animationComponent.clip = animationClip;
        _animationComponent.Play();
    }
}