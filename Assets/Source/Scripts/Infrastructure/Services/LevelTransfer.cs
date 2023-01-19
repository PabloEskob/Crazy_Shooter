using Source.Infrastructure;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
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
        }

        private void Start()
        {
            _switchScreen = GetComponent<SwitchScreen>();
            _switchScreen.DefeatScreen.ButtonToMap.Click += OnGoToMapButtonClick;
            _switchScreen.VictoryScreen.ButtonToMap.Click += OnGoToMapButtonClick;
        }

        private void OnGoToMapButtonClick(bool _isSuccess)
        {
            var gameConfig = _staticData.GetGameConfig();
            var lastCompletedLevelNumber = _storage.GetLevel();
            var currentLevelName = SceneManager.GetActiveScene().name;
            var lastCompletedLevelName = gameConfig.GetLevelNameByNumber(lastCompletedLevelNumber);

            if (gameConfig.LevelNames.Length - 1 != lastCompletedLevelNumber 
                && _isSuccess == true 
                && currentLevelName == lastCompletedLevelName)
            {
                _storage.SetLevel(lastCompletedLevelNumber + 1);
                _storage.Save();
            }

            _stateMachine.Enter<LoadMapSceneState>();
        }
    }
}