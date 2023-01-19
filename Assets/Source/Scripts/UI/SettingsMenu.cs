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
        [SerializeField] private Toggle _soundToggle;
        [SerializeField] private Toggle _musicToggle;
        [SerializeField] private Slider _cameraSensitivitySlider;
        [SerializeField] private Button _russianLanguageButton;
        [SerializeField] private Button _englishLaguageButton;
        [SerializeField] private Button _turkishLanguageButton;
        [SerializeField] private Button _exitButton;

        [Header("Slider values settings")]
        [SerializeField] private float _minValue = 0.1f;
        [SerializeField] private float _maxValue = 2.0f;

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

        private void OnLoad()
        {
            if (_storage.HasKeyFloat(SettingsNames.SensitivityKey))
                SetSliderValue(_storage.GetFloat(SettingsNames.SensitivityKey));
            else
                SetSliderValue(DefaultSliderValue);

            if (_storage.HasKeyInt(SettingsNames.SoundSettingsKey))
                _soundToggle.isOn = DataExtensions.IntToBool(_storage.GetInt(SettingsNames.SoundSettingsKey));

            if (_storage.HasKeyInt(SettingsNames.MusicSettingsKey))
                _musicToggle.isOn = DataExtensions.IntToBool(_storage.GetInt(SettingsNames.MusicSettingsKey));
        }

        private void OnEnable()
        {
            _exitButton.onClick.AddListener(Hide);
            _englishLaguageButton.onClick.AddListener(SwitchLanguage);
            _russianLanguageButton.onClick.AddListener(SwitchLanguage);
            _turkishLanguageButton.onClick.AddListener(SwitchLanguage);
            _soundToggle.onValueChanged.AddListener(OnToogleSwitched);
            _cameraSensitivitySlider.onValueChanged.AddListener(OnSensitivitySliderValueChanged);
        }

        private void OnDisable()
        {
            _storage.Save();
            _exitButton.onClick.RemoveListener(Hide);
            _englishLaguageButton.onClick.RemoveListener(SwitchLanguage);
            _russianLanguageButton.onClick.RemoveListener(SwitchLanguage);
            _turkishLanguageButton.onClick.RemoveListener(SwitchLanguage);
            _soundToggle.onValueChanged.RemoveListener(OnToogleSwitched);
            _cameraSensitivitySlider.onValueChanged.RemoveListener(OnSensitivitySliderValueChanged);
        }

        private void OnToogleSwitched(bool value) => 
            _storage.SetInt(SettingsNames.SoundSettingsKey, DataExtensions.BoolToInt(value));

        private void OnSensitivitySliderValueChanged(float value) =>
            _storage.SetFloat(SettingsNames.SensitivityKey, value);

        private void SwitchLanguage()
        {
            throw new NotImplementedException();
        }

        private void SetSliderValue(float value) =>
            _cameraSensitivitySlider.value = value;

        private void Hide() =>
            gameObject.SetActive(false);

        public void Show() =>
            gameObject.SetActive(true);
    }
}