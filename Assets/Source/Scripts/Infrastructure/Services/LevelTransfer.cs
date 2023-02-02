using Assets.Source.Scripts.Analytics;
using Source.Infrastructure;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
using Source.Scripts.StaticData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source.Scripts.Infrastructure.Services
{
    public class LevelTransfer : MonoBehaviour
    {

        [SerializeField] private SwitchScreen _switchScreen;

        private IGameStateMachine _stateMachine;
        private IStorage _storage;
        private IStaticDataService _staticData;
        private IAnalyticManager _analytic;
        private GameConfig _gameConfig;
        private int _lastCompletedLevelNumber;
        private string _currentLevelName;
        private int _currentLevelNumber;
        private string _lastCompletedLevelName;

        public int CurrentLevelNumber => _currentLevelNumber;
        public GameConfig GameConfig => _gameConfig;

        private void Awake()
        {
            _stateMachine = AllServices.Container.Single<IGameStateMachine>();
            _storage = AllServices.Container.Single<IStorage>();
            _staticData = AllServices.Container.Single<IStaticDataService>();
            _analytic = AllServices.Container.Single<IAnalyticManager>();
            _gameConfig = _staticData.GetGameConfig();
        }

        private void OnEnable()
        {
            _switchScreen.DefeatScreen.ButtonToMap.Click += OnGoToMapButtonClick;
            _switchScreen.VictoryScreen.ButtonToMap.Click += OnGoToMapButtonClick;
        }

        private void OnDisable()
        {
            _switchScreen.DefeatScreen.ButtonToMap.Click -= OnGoToMapButtonClick;
            _switchScreen.VictoryScreen.ButtonToMap.Click -= OnGoToMapButtonClick;
        }

        private void Start()
        {
            _lastCompletedLevelNumber = _storage.GetLevel();
            _currentLevelName = SceneManager.GetActiveScene().name;
            _currentLevelNumber = _gameConfig.GetLevelNumberByName(_currentLevelName);
            _lastCompletedLevelName = _gameConfig.GetLevelNameByNumber(_lastCompletedLevelNumber);
        }

        private void OnGoToMapButtonClick(bool _isSuccess)
        {

            if (_gameConfig.LevelNames.Length - 1 != _lastCompletedLevelNumber
                && _isSuccess == true
                && _currentLevelName == _lastCompletedLevelName)
            {
                _storage.SetLevel(_lastCompletedLevelNumber + 1);
                _storage.Save();
            }

            if (_isSuccess == false)
                _analytic.SendEventOnFail(_currentLevelNumber);
            else
                _analytic.SendEventOnLevelComplete(_currentLevelNumber);

            _stateMachine.Enter<LoadMapSceneState>();
        }
    }
}