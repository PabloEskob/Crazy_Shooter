using Assets.Source.Scripts.Localization;
using Lean.Localization;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Scripts.UI.Menus.Rewards
{
    public abstract class AdvertisementButton : MonoBehaviour
    {
        [SerializeField] protected Button Button;
        [SerializeField] private Text _buttonText;
        [SerializeField] private Text _defaultText;
        [SerializeField] private float _time;
        [SerializeField] private LanguageSwitcher _languageSwitcher;

        private Timer _timer;

        public event Action ButtonClicked;

        private void Awake()
        {
            _timer = new Timer();
        }

        private void OnEnable()
        {
            _timer.Started += OnTimerStart;
            _timer.Completed += OnTimerCompleted;
            _timer.Updated += OnTimerUpdated;
            Button.onClick.AddListener(OnAdButtonClick);
            _languageSwitcher.LanguageChanged += OnLaguageChanged;
        }

        private void OnDisable()
        {
            _timer.Started -= OnTimerStart;
            _timer.Completed -= OnTimerCompleted;
            _timer.Updated -= OnTimerUpdated;
            Button.onClick.RemoveListener(OnAdButtonClick);
            _languageSwitcher.LanguageChanged -= OnLaguageChanged;
        }

        private void Start()
        {
            SetText(_defaultText.text);
        }

        private void Update() => _timer.Tick(Time.deltaTime);

        private void OnTimerStart() => Button.interactable = false;

        private void OnTimerUpdated() => DisplayTime();

        private void OnLaguageChanged() => SetText(_defaultText.text);

        private void OnTimerCompleted()
        {
            SetText(_defaultText.text);
            Button.interactable = true;
            _timer.Stop();
        }

        private void OnAdButtonClick()
        {
            ButtonClicked?.Invoke();
            _timer.Start(_time);
        }

        private void SetText(string text) => _buttonText.text = text;

        private void DisplayTime()
        {
            float minutes = Mathf.FloorToInt(_timer.TimeLeft / 60);
            float seconds = Mathf.FloorToInt(_timer.TimeLeft % 60);

            SetText(string.Format("{0:00}:{1:00}", minutes, seconds));
        }
    }
}
