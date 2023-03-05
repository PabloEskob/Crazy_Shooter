using Source.Infrastructure;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
using Source.Scripts.Ui;
using System.Collections;
using UnityEngine;

public class StartLevelButtonsView : MonoBehaviour
{
    [SerializeField] private MainMap _mainMap;
    
    private StartLevelButton[] _levelButtons;

    [Header("Color Settings")]
    [SerializeField] Color _passedLevelButtonColor = Color.grey;
    [SerializeField] Color _notAvailiableLevelButtonColor = Color.red;

    private IStorage Storage => _mainMap.Storage;

    private void Awake() => _levelButtons = _mainMap.StartLevelButtons;

    private void Start()
    {
        int currentLevelNumber = Storage.GetLevel();

        foreach (var levelButton in _levelButtons)
        {
            if (levelButton.LevelNumber < currentLevelNumber)
                SetButtonImageColor(levelButton, _passedLevelButtonColor);

            if (levelButton.LevelNumber > currentLevelNumber)
            {
                SetButtonImageColor(levelButton, _notAvailiableLevelButtonColor);
                levelButton.StartButton.interactable = false;
            }

        }
    }

    private void SetButtonImageColor(StartLevelButton button, Color color)
    {
        button.Image.color = color;
    }
}
