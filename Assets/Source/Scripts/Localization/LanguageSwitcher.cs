using System;
using System.Reflection;
using Lean.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Scripts.Localization
{
    public class LanguageSwitcher : MonoBehaviour
    {
        [SerializeField] private LeanLocalization _leanLocalization;
        [SerializeField] private Button _nextLanguageButton;
        [SerializeField] private Button _previousLanguageButton;
        [SerializeField] private LeanLanguage[] _languages;

        private void OnEnable()
        {
            _nextLanguageButton.onClick.AddListener(OnNextButtonClick);
            _previousLanguageButton.onClick.AddListener(OnPreviousButtonClick);
        }

        private void OnDisable()
        {
            _nextLanguageButton.onClick.RemoveListener(OnNextButtonClick);
            _previousLanguageButton.onClick.RemoveListener(OnPreviousButtonClick);
        }

        private void OnNextButtonClick()
        {

            for (int i = 0; i < _languages.Length; i++)
            {
                if (GetCurrentLanguage() == _languages[i].name)
                {
                    int index = i + 1;

                    if (index > _languages.Length - 1)
                        index = 0;

                    SetLanguage(_languages[index].name);
                    return;
                }
            }
        }

        private void OnPreviousButtonClick()
        {
            for (int i = 0; i < _languages.Length; i++)
            {
                if (GetCurrentLanguage() == _languages[i].name)
                {
                    int index = i - 1;

                    if (index < 0)
                        index = _languages.Length - 1;

                    SetLanguage(_languages[index].name);
                    return;
                }
            }
        }

        private string GetCurrentLanguage() =>
            _leanLocalization.CurrentLanguage;

        private void SetLanguage(string languageName) => 
            _leanLocalization.SetCurrentLanguage(languageName);
    }
}