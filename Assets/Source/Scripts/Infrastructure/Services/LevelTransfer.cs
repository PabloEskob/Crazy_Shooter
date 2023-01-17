using Source.Infrastructure;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

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
            _switchScreen.DefeatScreen.ButtonToMap.Click -= GoToMap;
            _switchScreen.VictoryScreen.ButtonToMap.Click -= GoToMap;
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
            _switchScreen.DefeatScreen.ButtonToMap.Click += GoToMap;
            _switchScreen.VictoryScreen.ButtonToMap.Click += GoToMap;
        }

        private void GoToMap(bool _isSuccess)
        {
            Debug.Log($"current level - {_storage.GetLevel()}");
            Debug.Log($"level config length - {_staticData.GetGameConfig().LevelConfigs.Length}");
            if (_staticData.GetGameConfig().LevelConfigs.Length - 1 != _storage.GetLevel() && _isSuccess == true)
            {
                _storage.SetLevel(_storage.GetLevel() + 1);
                _storage.Save();
            }

            _stateMachine.Enter<LoadMapSceneState>();
        }
    }
}