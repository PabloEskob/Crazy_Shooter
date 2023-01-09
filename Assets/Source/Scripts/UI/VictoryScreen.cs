using InfimaGames.LowPolyShooterPack;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField] private AnimationClip _down;

    private Character _player;
    private CanvasGroup _canvasGroup;
    private Animation _animationComponent;
    private bool _cursorLocked;
    private Canvas _canvas;

    private void Start()
    {
        _canvas = GetComponentInParent<Canvas>();
        _canvas.sortingOrder = -1;
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
        _animationComponent = GetComponent<Animation>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Character>();
    }

    public void Show()
    {
        _canvas.sortingOrder = 1;
        CursorVisibility();
        _animationComponent.clip = _down;
        _animationComponent.Play();
    }

    private void CursorVisibility()
    {
        _player.LockCursor();
        Cursor.visible = true;
        Cursor.lockState = _cursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
    }
}