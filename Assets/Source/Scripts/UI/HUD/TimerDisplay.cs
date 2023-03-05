using Assets.Source.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private SurviveTimer _surviveTimer;

    private Timer _timer;

    private void Awake() => 
        _timer = _surviveTimer.Timer;

    private void Start() => 
        _timer.Updated += OnTimeUpdate;

    private void OnDisable() => 
        _timer.Updated -= OnTimeUpdate;

    private void OnTimeUpdate() => 
        Display();

    private void Display()
    {
        float minutes = Mathf.FloorToInt(_timer.TimeLeft / 60);
        float seconds = Mathf.FloorToInt(_timer.TimeLeft % 60);

        _timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (_timer.TimeLeft <= 0)
            _timeText.text = "00:00";
    }
}
