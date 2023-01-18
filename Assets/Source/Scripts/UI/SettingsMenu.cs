using Assets.Source.Scripts.UI;
using Source.Infrastructure;
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

        IStorage _storage;

        private void Awake()
        {
            _storage = AllServices.Container.Single<IStorage>();
            _cameraSensitivitySlider.minValue = 0.1f;
            _cameraSensitivitySlider.maxValue = 2.0f;
            _cameraSensitivitySlider.value = _storage.GetFloat(SettingsNames.SensitivityKey);

            Hide();
        }

        private void OnEnable()
        {
            _exitButton.onClick.AddListener(Hide);
            _englishLaguageButton.onClick.AddListener(SwitchLanguage);
            _russianLanguageButton.onClick.AddListener(SwitchLanguage);
            _turkishLanguageButton.onClick.AddListener(SwitchLanguage);
            _cameraSensitivitySlider.onValueChanged.AddListener(SaveSliderSettings);
        }

        private void OnDisable()
        {
            _storage.Save();
            _exitButton.onClick.RemoveListener(Hide);
            _englishLaguageButton.onClick.RemoveListener(SwitchLanguage);
            _russianLanguageButton.onClick.RemoveListener(SwitchLanguage);
            _turkishLanguageButton.onClick.RemoveListener(SwitchLanguage);
            _cameraSensitivitySlider.onValueChanged.RemoveListener(SaveSliderSettings);
        }

        private void SaveSliderSettings(float value) => 
            _storage.SetFloat(SettingsNames.SensitivityKey, value);

        private void SwitchLanguage()
        {
            throw new NotImplementedException();
        }

        private void Hide() => 
            gameObject.SetActive(false);

        public void Show() => 
            gameObject.SetActive(true);
    }
}