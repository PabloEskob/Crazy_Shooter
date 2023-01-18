using Source.Infrastructure;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartLevelButtonsView : MonoBehaviour
{
    [SerializeField] private StartLevelButton[] _levelButtons;

    [Header("Color Settings")]
    [SerializeField] Color _passedLevelButtonColor = Color.grey;
    [SerializeField] Color _notAvailiableLevelButtonColor = Color.red;

    private IStorage _storage;

    private void Start()
    {
        _storage = AllServices.Container.Single<IStorage>();
        int currentLevelNumber = _storage.GetLevel();

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
