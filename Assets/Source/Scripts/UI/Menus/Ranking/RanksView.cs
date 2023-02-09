using Agava.YandexGames;
using Source.Scripts.Data;
using UnityEngine;

namespace Assets.Source.Scripts.UI.Menus.Ranking
{
    public class RanksView : MonoBehaviour
    {
        [SerializeField] private ChallengerView _template;
        [SerializeField] private GameObject _container;

        private const string UnknownPlayerName = "Неизвестный игрок";
        private string _playerID;
        private string _playerName;

        private void OnEnable()
        {
            ClearEntri();
            TryShowChalengers();

        }

        private void GetPlayerInfo()
        {
            PlayerAccount.GetProfileData((result) =>
            {
                _playerID = result.uniqueID;
                _playerName = result.publicName;
            });
        }

        public void AddChallengers()
        {
            GetPlayerInfo();

            Leaderboard.GetEntries(LeaderboardName.Name, (result) =>
            {
                foreach (var entry in result.entries)
                {
                    var view = Instantiate(_template, _container.transform);

                    if (string.IsNullOrEmpty(entry.player.publicName))
                        view.SetName(UnknownPlayerName);
                    else
                        view.SetName(entry.player.publicName);

                    view.SetScore(entry.score);
                    view.SetRank(entry.rank);

                    if (_playerName == entry.player.publicName)
                    {
                        view.EnablePlayerBackground();
                        view.SetTextColor();
                    }

                    view.TrySetRankIcon(entry.rank);
                   
                    Debug.Log($"ExtraData {entry.extraData}");
                    
                }
            });
        }

        public void ClearEntri()
        {
            if (_container.transform.childCount > 0)
                foreach (Transform child in _container.transform)
                    Destroy(child.gameObject);
        }

        public void TryShowChalengers()
        {
#if (!UNITY_EDITOR && UNITY_WEBGL)
            if (PlayerAccount.IsAuthorized)
                AddChallengers();
#endif
        }
    }
}
