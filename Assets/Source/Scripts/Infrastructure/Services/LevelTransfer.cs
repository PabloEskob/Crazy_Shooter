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
        private SwitchScreen _switchScreen;
        private IGameStateMachine _stateMachine;
        private IStorage _storage;
        private IStaticDataService _staticData;
        private IAnalyticManager _analytic;

        private void OnDisable()
        {
            _switchScreen.DefeatScreen.ButtonToMap.Click -= OnGoToMapButtonClick;
            _switchScreen.VictoryScreen.ButtonToMap.Click -= OnGoToMapButtonClick;
        }

        private void Awake()
        {
            _stateMachine = AllServices.Container.Single<IGameStateMachine>();
            _storage = AllServices.Container.Single<IStorage>();
            _staticData = AllServices.Container.Single<IStaticDataService>();
            _analytic = AllServices.Container.Single<IAnalyticManager>();
        }

        private void Start()
        {
            _switchScreen = GetComponent<SwitchScreen>();
            _switchScreen.DefeatScreen.ButtonToMap.Click += OnGoToMapButtonClick;
            _switchScreen.VictoryScreen.ButtonToMap.Click += OnGoToMapButtonClick;
        }

        private void OnGoToMapButtonClick(bool _isSuccess)
        {
            GameConfig gameConfig = _staticData.GetGameConfig();
            int lastCompletedLevelNumber = _storage.GetLevel();
            string currentLevelName = SceneManager.GetActiveScene().name;
            int currentLevelNumber = gameConfig.GetLevelNumberByName(currentLevelName);
            string lastCompletedLevelName = gameConfig.GetLevelNameByNumber(lastCompletedLevelNumber);

            if (gameConfig.LevelNames.Length - 1 != lastCompletedLevelNumber
                && _isSuccess == true
                && currentLevelName == lastCompletedLevelName)
            {
                _storage.SetLevel(lastCompletedLevelNumber + 1);
                _storage.Save();
            }

            if (_isSuccess == false)
                _analytic.SendEventOnFail(currentLevelNumber);
            else
                _analytic.SendEventOnLevelComplete(currentLevelNumber);

            _stateMachine.Enter<LoadMapSceneState>();
        }
    }
}