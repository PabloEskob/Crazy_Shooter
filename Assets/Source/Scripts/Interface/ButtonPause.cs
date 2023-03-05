using System;
using UnityEngine;

public class ButtonPause : MonoBehaviour
{
    private bool _isPause = true;
    private bool _cursor;
    private bool _cursorLocked;
    public event Action<bool> Paused;
    
    public void SwitchPanel()
    {
        switch (_isPause)
        {
            case true:
                _cursor = Cursor.visible;

                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;
            case false when !_cursor:
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                break;
        }

        ProjectContext.Instance.PauseService.SetPaused(_isPause);
        Paused?.Invoke(_isPause);
        _isPause = !_isPause;
    }
}