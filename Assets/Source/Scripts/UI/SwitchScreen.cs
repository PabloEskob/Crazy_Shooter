using System.Collections.Generic;
using InfimaGames.LowPolyShooterPack;
using UnityEngine;

public class SwitchScreen : MonoBehaviour
{
    [SerializeField] private AnimationClip _animationClipVictory;
    [SerializeField] private AnimationClip _animationClipDefeat;

    private VictoryScreen _victoryScreen;
    private DefeatScreen _defeatScreen;
    private Character _player;
    private bool _cursorLocked;
    private Canvas _canvas;

    public VictoryScreen VictoryScreen => _victoryScreen;
    public DefeatScreen DefeatScreen => _defeatScreen;

    private void Awake()
    {
        _victoryScreen = GetComponentInChildren<VictoryScreen>();
        _defeatScreen = GetComponentInChildren<DefeatScreen>();
        _canvas = GetComponentInParent<Canvas>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Character>();
    }

    private void Start() =>
        _canvas.sortingOrder = -1;

    public void ShowVictoryScreen()
    {
        CursorVisibility();
        _victoryScreen.Show(_animationClipVictory);
    }

    public void ShowDefeatScreen()
    {
        CursorVisibility();
        _defeatScreen.Show(_animationClipDefeat);
    }

    private void CursorVisibility()
    {
        _canvas.sortingOrder = 1;
        _player.LockCursor();
        Cursor.visible = true;
        Cursor.lockState = _cursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
    }
}