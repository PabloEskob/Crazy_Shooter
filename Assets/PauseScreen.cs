using Source.Scripts.Ui;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    private ButtonPause _buttonPause;
    private SettingsMenu _settingsMenu;
    private SwitchScreen _switchScreen;

    private void OnEnable() =>
        _buttonPause.Paused += OpenMenuSettings;

    private void OnDisable() =>
        _buttonPause.Paused -= OpenMenuSettings;

    private void Awake()
    {
        _switchScreen = GetComponentInChildren<SwitchScreen>();
        _buttonPause = GetComponentInChildren<ButtonPause>();
        _settingsMenu = GetComponentInChildren<SettingsMenu>();
    }

    public void OpenPanel() =>
        _buttonPause.SwitchPanel();

    private void OpenMenuSettings(bool open)
    {
        _switchScreen.ChangeSortingOrderCanvas(open ? 1 : 0);
        _settingsMenu.gameObject.SetActive(open);
    }
}