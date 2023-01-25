using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameDisplay : MonoBehaviour
{
    [SerializeField] private Text _dysplayedName;

    private string _playerID;
    private string _playerName;

    private string _defaultPlayerName;

    private void Awake()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        if (PlayerAccount.IsAuthorized)
        {
            GetPlayerInfo();
            _dysplayedName.text = _playerName;
        }
#endif
        _dysplayedName.text = "Unkknown Player";
    }

    private void GetPlayerInfo()
    {
        PlayerAccount.GetProfileData((result) =>
        {
            _playerID = result.uniqueID;
            _playerName = result.publicName;
        });
    }

    private void SetDefaultPlayerName()
    {

    }
}
