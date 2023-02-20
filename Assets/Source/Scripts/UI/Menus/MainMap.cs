using Assets.Source.Scripts.UI.Menus.Ranking;
using Assets.Source.Scripts.UI.Menus.Rewards;
using Source.Infrastructure;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.Ui
{
    public class MainMap : MonoBehaviour
    {
        [Header("Menu Buttons")]
        [SerializeField] private Button _rankingMenuButton;
        [SerializeField] private Button _upgradeMenuButton;
        [SerializeField] private Button _settingsMenuButton;
        [SerializeField] private Button _dailyRewardMenuButton;
        [Space(20)]
        [SerializeField] private StartLevelButton[] _startLevelButtons;

        [Header("Menus")]
        [SerializeField] private UpgradeMenu _upgradeMenu;
        [SerializeField] private SettingsMenu _settingsMenu;
        [SerializeField] private RankingMenu _rankingMenu;
        [SerializeField] private DailyRewardsMenu _dailyRewardMenu;

        private SceneLoader _sceneLoader;
        private GameStateMachine _stateMachine;
        private IStorage _storage;

        public IStorage Storage => _storage;
        public StartLevelButton[] StartLevelButtons => _startLevelButtons;

        public void Construct(SceneLoader sceneLoader, GameStateMachine stateMachine, IStorage storage)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _storage = storage;
        }

        private void OnEnable()
        {
            _upgradeMenuButton.onClick.AddListener(OpenUpgradeMenu);
            _upgradeMenu.Activated += OnUpgradeMenuActivated;
            _settingsMenuButton.onClick.AddListener(OpenSettingsMenu);
            _settingsMenu.Activated += OnSettingsMenuActivated;
            _rankingMenuButton.onClick.AddListener(OpenRankingMenu);
            _rankingMenu.Activated += OnRankingMenuActivated;
            _dailyRewardMenuButton.onClick.AddListener(OpenDailyRewardMenu);
            _dailyRewardMenu.Activated += OnRankingMenuActivated;
            

            foreach (var button in _startLevelButtons)
                button.Clicked += StartLevel;
        }

        private void OnDisable()
        {
            _upgradeMenuButton.onClick.RemoveListener(OpenUpgradeMenu);
            _upgradeMenu.Activated -= OnUpgradeMenuActivated;
           _settingsMenuButton.onClick.RemoveListener(OpenSettingsMenu);
            _settingsMenu.Activated -= OnSettingsMenuActivated;
            _rankingMenuButton.onClick.AddListener(OpenRankingMenu);
            _rankingMenu.Activated -= OnRankingMenuActivated;
            _dailyRewardMenuButton.onClick.AddListener(OpenDailyRewardMenu);
            _dailyRewardMenu.Activated += OnRankingMenuActivated;

            foreach (var button in _startLevelButtons)
                button.Clicked += StartLevel;
        }

        private void OpenDailyRewardMenu() => 
            StartCoroutine(ActivateMenu(_dailyRewardMenu.gameObject));

        private void OpenRankingMenu() => 
            StartCoroutine(ActivateMenu(_rankingMenu.gameObject));

        private void OpenSettingsMenu() => 
            StartCoroutine(ActivateMenu(_settingsMenu.gameObject));

        private void OpenUpgradeMenu() => 
            StartCoroutine(ActivateMenu(_upgradeMenu.gameObject));

        private void StartLevel(int level) =>
            _stateMachine.Enter<LoadLevelState>(level);

        private IEnumerator ActivateMenu(GameObject gameObject)
        {
            while(gameObject.activeInHierarchy == false) 
            {
                gameObject.SetActive(true);
                yield return null;
            }
        }

        private void OnSettingsMenuActivated() => 
            StopCoroutine(ActivateMenu(_settingsMenu.gameObject));

        private void OnUpgradeMenuActivated() => 
            StopCoroutine(ActivateMenu(_upgradeMenu.gameObject));

        private void OnRankingMenuActivated() => 
            StopCoroutine(ActivateMenu(_rankingMenu.gameObject));

        private void OnDalyRewardMenuActivated() => 
            StopCoroutine(ActivateMenu(_dailyRewardMenu.gameObject));
    }
}