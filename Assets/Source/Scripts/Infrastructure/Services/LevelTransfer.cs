using Source.Infrastructure;
using UnityEngine;

namespace Source.Scripts.Infrastructure.Services
{
    public class LevelTransfer : MonoBehaviour
    {
        private SwitchScreen _switchScreen;
        private IGameStateMachine _stateMachine;

        private void OnDisable()
        {
            _switchScreen.DefeatScreen.ButtonToMap.Click -= GoToMap;
            _switchScreen.VictoryScreen.ButtonToMap.Click -= GoToMap;
        }

        private void Awake() =>
            _stateMachine = AllServices.Container.Single<IGameStateMachine>();

        private void Start()
        {
            _switchScreen = GetComponent<SwitchScreen>();
            _switchScreen.DefeatScreen.ButtonToMap.Click += GoToMap;
            _switchScreen.VictoryScreen.ButtonToMap.Click += GoToMap;
        }

        private void GoToMap() =>
            _stateMachine.Enter<LoadMapSceneState>();
    }
}