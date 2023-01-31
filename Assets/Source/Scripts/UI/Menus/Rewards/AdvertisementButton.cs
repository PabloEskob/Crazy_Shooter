using Assets.Source.Scripts.Localization;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Scripts.UI.Menus.Rewards
{
    public abstract class AdvertisementButton : Button
    {
        [SerializeField] private Text _text;
        [SerializeField] private Text _defaultText;
        [SerializeField] private float _time;
        [SerializeField] private LanguageSwitcher _languageSwitcher;

        private Timer _timer;

        public event Action ButtonClicked;

        protected override void Awake()
        {
            base.Awake();
            if (Application.isEditor) return;

            _timer = new Timer();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            if (Application.isEditor) return;

            _timer.Started += OnTimerStart;
            _timer.Completed += OnTimerCompleted;
            _timer.Updated += OnTimerUpdated;
            onClick.AddListener(OnAdButtonClick);
            _languageSwitcher.LanguageChanged += OnLaguageChanged;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            if (Application.isEditor) return;

            _timer.Started -= OnTimerStart;
            _timer.Completed -= OnTimerCompleted;
            _timer.Updated -= OnTimerUpdated;
            onClick.RemoveListener(OnAdButtonClick);
            _languageSwitcher.LanguageChanged -= OnLaguageChanged;
        }

        protected override void Start()
        {
            base.Start();
            if (Application.isEditor) return;

            SetText(_defaultText.text);
        }

        private void Update() => _timer?.Tick(Time.deltaTime);

        private void OnTimerStart() => interactable = false;

        private void OnTimerUpdated() => DisplayTime();

        private void OnLaguageChanged() => SetText(_defaultText.text);

        private void OnTimerCompleted()
        {
            SetText(_defaultText.text);
            interactable = true;
            _timer.Stop();
        }

        private void OnAdButtonClick()
        {
            ButtonClicked?.Invoke();
            _timer.Start(_time);
        }

        private void SetText(string text) => _text.text = text;

        private void DisplayTime()
        {
            float minutes = Mathf.FloorToInt(_timer.TimeLeft / 60);
            float seconds = Mathf.FloorToInt(_timer.TimeLeft % 60);

            SetText(string.Format("{0:00}:{1:00}", minutes, seconds));
        }
    }
}
