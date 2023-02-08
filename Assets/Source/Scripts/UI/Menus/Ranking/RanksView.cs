using Agava.YandexGames;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Scripts.UI.Menus.Ranking
{
    public class RanksView : MonoBehaviour
    {
        [SerializeField] private ChallengerView _template;
        [SerializeField] private GameObject _container;
        [SerializeField][Range(0, 10)] private float _scrollTime;

        private const string UnknownPlayerName = "Неизвестный игрок";
        private const float ResetDelay = 0.4f;
        private ScrollRect _scrollRect;
        private bool _canScroll = true;
        private bool _isAnimationStoped;
        private float _verticalLastPosition = 1f;
        private float _verticalStartPosition = 0f;
        private string _playerID;
        private string _playerName;

        private void Awake()
        {
            _scrollRect = GetComponentInChildren<ScrollRect>();
            ResetScrollRectPosition();
        }

        private void OnEnable()
        {
            ClearEntri();
            TryShowChalengers();
            StartCoroutine(ResetCoroutine());
        }

        private void OnDisable()
        {
            _canScroll = true;
            ResetScrollRectPosition();
        }

        private void Update()
        {
            if (_isAnimationStoped)
                StartCoroutine(SlideCoroutine(_scrollTime));
        }

        private IEnumerator SlideCoroutine(float time)
        {
            float Timer = 0;

            if (_canScroll)
                while (_scrollRect.verticalNormalizedPosition < _verticalLastPosition)
                {
                    _scrollRect.verticalNormalizedPosition = Mathf.Lerp(_verticalStartPosition, _verticalLastPosition, Timer / time);
                    yield return null;
                    Timer += Time.deltaTime;
                }

            _canScroll = false;
            Debug.Log(_canScroll);

        }

        private IEnumerator ResetCoroutine()
        {
            yield return new WaitForSeconds(ResetDelay);
            ResetScrollRectPosition();
        }

        private void GetPlayerInfo()
        {
            PlayerAccount.GetProfileData((result) =>
            {
                _playerID = result.uniqueID;
                _playerName = result.publicName;
            });
        }

        private void ResetScrollRectPosition()
        {
            _scrollRect.verticalNormalizedPosition = _verticalStartPosition;
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
                }
            });
        }

        public void ClearEntri()
        {
            if (_container.transform.childCount > 0)
                foreach (Transform child in _container.transform)
                    Destroy(child.gameObject);
        }

        public void OnStartAnimation()
        {
            _isAnimationStoped = false;
        }

        public void OnEndAnimation()
        {
            _isAnimationStoped = true;
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
