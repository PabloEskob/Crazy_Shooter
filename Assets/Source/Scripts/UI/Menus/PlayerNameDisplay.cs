using Agava.YandexGames;
using Assets.Source.Scripts;
using Assets.Source.Scripts.Localization;
using Source.Scripts.Data;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameDisplay : MonoBehaviour
{
    [SerializeField] private Text _displayedName;
    [SerializeField] private Text _defaultName;
    [SerializeField] private Image _playerAvatar;
    [SerializeField] private LanguageSwitcher _languageSwitcher;

    private string _playerID;
    private string _playerName;
    private string _defaultPlayerName;


    private void Start()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        Invoke(nameof(CheckNames), 3f);
#endif

#if UNITY_EDITOR
        SetPlayerName(_defaultName.text);
#endif

    }

    private void OnEnable() => _languageSwitcher.LanguageChanged += OnLanguageChanged;

    private void OnDisable() => _languageSwitcher.LanguageChanged -= OnLanguageChanged;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            CheckNames();
    }

    private void OnLanguageChanged()
    {
        if (PlayerAccount.IsAuthorized == false)
            SetPlayerName(_defaultName.text);
    }

    private void CheckNames()
    {
        if (PlayerAccount.IsAuthorized)
        {
            GetPlayerInfo();
            SetPlayerName(_playerName);
            Debug.Log($"PlayerName - {_playerName}");
        }
        else
        {
            Debug.Log($"Player is not authorized");
            SetPlayerName(_defaultName.text);
        }
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
