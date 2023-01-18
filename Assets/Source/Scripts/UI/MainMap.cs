using Source.Infrastructure;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.Ui
{
    public class MainMap : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button _upgradeMenuButton;
        [SerializeField] private Button _settingsMenuButton;
        [SerializeField] private StartLevelButton[] _startLevelButtons;

        [Header("Menus")]
        [SerializeField] private UpgradeMenu _upgradeMenu;
        [SerializeField] private SettingsMenu _settingsMenu;

        private SceneLoader _sceneLoader;
        private GameStateMachine _stateMachine;
        private IStorage _storage;

        public void Cunstruct(SceneLoader sceneLoader, GameStateMachine stateMachine, IStorage storage)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _storage = storage;
        }

        private void OnEnable()
        {
            _upgradeMenuButton.onClick.AddListener(OpenUpgradeMenu);
            _settingsMenuButton.onClick.AddListener(OpenSettingsMenu);
            
            foreach (var button in _startLevelButtons)
                button.Clicked += StartLevel;
        }

        private void OnDisable()
        {
            _upgradeMenuButton.onClick.RemoveListener(OpenUpgradeMenu);
            _settingsMenuButton.onClick.RemoveListener(OpenSettingsMenu);

            foreach (var button in _startLevelButtons)
                button.Clicked += StartLevel;
        }

        private void OpenSettingsMenu() => 
            _settingsMenu.Show();

        private void OpenUpgradeMenu() =>
            _upgradeMenu.Show();

        private void StartLevel(int level) =>
            _stateMachine.Enter<LoadLevelState>(level);
    }
}