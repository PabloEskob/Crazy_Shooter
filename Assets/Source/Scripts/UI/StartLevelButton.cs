using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartLevelButton : MonoBehaviour
{
    [Min(1)]
    [SerializeField] private int _levelNumber;
    [SerializeField] private Button _startButton;
    [SerializeField] private TMP_Text _buttonText;
    [SerializeField] private Image _image;

    public int LevelNumber => _levelNumber;
    public Button StartButton => _startButton;
    public Image Image => _image;

    public event Action<int> Clicked;

    private void OnEnable() =>
        _startButton.onClick.AddListener(OnButtonClick);

    private void OnDisable() =>
        _startButton.onClick.AddListener(OnButtonClick);

    private void Start() =>
        SetText();

    private void OnButtonClick() =>
        Clicked(_levelNumber);

    private void SetText() =>
        _buttonText.text += _levelNumber;
}
