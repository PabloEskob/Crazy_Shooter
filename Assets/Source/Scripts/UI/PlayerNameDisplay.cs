using Agava.YandexGames;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameDisplay : MonoBehaviour
{
    [SerializeField] private Text _displayedName;

    private string _playerID;
    private string _playerName;
    private string _defaultPlayerName;

    private const string UnknownPlayerName = "Unkknown Player";

    private void Awake()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        //if (PlayerAccount.IsAuthorized)
        //{
        //    GetPlayerInfo();
        //    SetPlayerName(_playerName);
        //}
        //else
        //{
        //    SetPlayerName(UnknownPlayerName);
        //}
#endif

#if UNITY_EDITOR
        SetPlayerName(UnknownPlayerName);
#endif

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
