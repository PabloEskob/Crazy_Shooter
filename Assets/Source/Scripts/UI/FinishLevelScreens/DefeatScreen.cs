using UnityEngine;

public class DefeatScreen : Screen
{
    [SerializeField] private ButtonRestart _buttonRestart;
    public ButtonRestart ButtonToRestart => _buttonRestart;
}