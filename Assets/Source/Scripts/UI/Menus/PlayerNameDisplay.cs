using Agava.YandexGames;
using Assets.Source.Scripts.Localization;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameDisplay : MonoBehaviour
{
    [SerializeField] private Text _displayedName;
    [SerializeField] private Text _defaultName;
    [SerializeField] private LanguageSwitcher _languageSwitcher;

    private string _playerID;
    private string _playerName;
    private string _defaultPlayerName;


    private void Start()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        if (PlayerAccount.IsAuthorized)
        {
            GetPlayerInfo();
            SetPlayerName(_playerName);
        }
        else
        {
            SetPlayerName(_defaultName.text);
        }
#endif

#if UNITY_EDITOR
        SetPlayerName(_defaultName.text);
#endif

    }

    private void OnEnable()
    {
        _languageSwitcher.LanguageChanged += OnLanguageChanged;
    }

    private void OnDisable()
    {
        _languageSwitcher.LanguageChanged -= OnLanguageChanged;
    }

    private void OnLanguageChanged()
    {
        SetPlayerName(_defaultName.text);
    }

    private void GetPlayerInfo()
    {
        PlayerAccount.GetProfileData((result) =>
        {
            _playerID = result.uniqueID;
            _playerName = result.publicName;
        });
    }

    private void SetPlayerName(string name) => 
        _displayedName.text = name;
}
