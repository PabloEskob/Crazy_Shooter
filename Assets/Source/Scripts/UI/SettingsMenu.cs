using Assets.Source.Scripts.UI;
using Source.Infrastructure;
using Source.Scripts.Data;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.Ui
{
    public class SettingsMenu : MonoBehaviour
    {
        [SerializeField] private Slider _soundVolumeSlider;
        [SerializeField] private Slider _musicVolumeSlider;
        [SerializeField] private Slider _cameraSensitivitySlider;
        [SerializeField] private Button _nextLanguageButton;
        [SerializeField] private Button _previousLaguageButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Text _languageText;

        [Header("Sensitivity slider values settings")]
        [SerializeField] private float _minValue = 0.1f;
        [SerializeField] private float _maxValue = 2.0f;

        private const string RussianLanguage = "Русский";
        private const string EnglishLanguage = "English";
        private const string TürkLanguage = "Türk";

        private string[] _languageTexts = new string[3] {RussianLanguage, EnglishLanguage, TürkLanguage };

        IStorage _storage;

        private const int DefaultSliderValue = 1;

        private void Awake()
        {
            _storage = AllServices.Container.Single<IStorage>();
            _cameraSensitivitySlider.minValue = _minValue;
            _cameraSensitivitySlider.maxValue = _maxValue;
            OnLoad();
            Hide();
        }

        private void OnEnable()
        {
            _exitButton.onClick.AddListener(Hide);
            _nextLanguageButton.onClick.AddListener(SwitchLanguage);
            _soundVolumeSlider.onValueChanged.AddListener(OnSoundSliderValueChanged);
            _musicVolumeSlider.onValueChanged.AddListener(OnMusicSliderValueChanged);
            _cameraSensitivitySlider.onValueChanged.AddListener(OnSensitivitySliderValueChanged);
        }

        private void OnDisable()
        {
            _storage.Save();
            _exitButton.onClick.RemoveListener(Hide);
            _nextLanguageButton.onClick.RemoveListener(SwitchLanguage);
            _soundVolumeSlider.onValueChanged.RemoveListener(OnSoundSliderValueChanged);
            _musicVolumeSlider.onValueChanged.RemoveListener(OnMusicSliderValueChanged);
            _cameraSensitivitySlider.onValueChanged.RemoveListener(OnSensitivitySliderValueChanged);
        }

        private void OnSoundSliderValueChanged(float value) =>
            _storage.SetFloat(SettingsNames.SoundSettingsKey, value);

        private void OnMusicSliderValueChanged(float value) =>
            _storage.SetFloat(SettingsNames.MusicSettingsKey, value);

        private void OnSensitivitySliderValueChanged(float value) =>
            _storage.SetFloat(SettingsNames.SensitivityKey, value);

        private void SwitchLanguage()
        {
            
        }

        private void SetSliderValue(Slider slider, float value) =>
            slider.value = value;

        private void Hide() =>
            gameObject.SetActive(false);

        private void OnLoad()
        {
            if (_storage.HasKeyFloat(SettingsNames.SensitivityKey))
                SetSliderValue(_cameraSensitivitySlider, _storage.GetFloat(SettingsNames.SensitivityKey));
            else
                SetSliderValue(_cameraSensitivitySlider, DefaultSliderValue);

            if (_storage.HasKeyFloat(SettingsNames.SoundSettingsKey))
                SetSliderValue(_soundVolumeSlider, _storage.GetFloat(SettingsNames.SoundSettingsKey));
            else
                SetSliderValue(_soundVolumeSlider, DefaultSliderValue);

            if (_storage.HasKeyFloat(SettingsNames.MusicSettingsKey))
                SetSliderValue(_musicVolumeSlider, _storage.GetFloat(SettingsNames.MusicSettingsKey));
            else
                SetSliderValue(_musicVolumeSlider, DefaultSliderValue);
        }

        public void Show() =>
            gameObject.SetActive(true);
    }
}