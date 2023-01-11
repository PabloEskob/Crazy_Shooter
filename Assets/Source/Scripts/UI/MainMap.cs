using Source.Infrastructure;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.Ui
{
    public class MainMap : MonoBehaviour
    {
        [SerializeField] private Button _upgradeMenuButton;
        [SerializeField] private Button _startLevelButton;
        [SerializeField] private UpgradeMenu _upgradeMenu;

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
            _startLevelButton.onClick.AddListener(StartLevel);
        }

        private void OnDisable()
        {
            _upgradeMenuButton.onClick.RemoveListener(OpenUpgradeMenu);
            _startLevelButton.onClick.RemoveListener(StartLevel);
        }

        private void OpenUpgradeMenu() => 
            _upgradeMenu.Show();

        private void StartLevel() => 
            _stateMachine.Enter<LoadLevelState>();
    }
}