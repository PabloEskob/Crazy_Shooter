using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Scripts.UI.LevelRewards
{
    public class RewardDisplay : MonoBehaviour
    {
        [SerializeField] private VictoryScreen _victoryScreen;
        [SerializeField] private LevelRewardView _rewardTemplate;
        [SerializeField] private Transform _container;


        private void OnEnable()
        {
            _victoryScreen.Loaded += OnLoaded;
        }

        private void OnDisable()
        {
            _victoryScreen.Loaded -= OnLoaded;
        }

        private void OnLoaded(string amount)
        {
            Show(amount);
        }

        public void Show(string amount)
        {
            LevelRewardView rewardView = Instantiate(_rewardTemplate, _container);
            rewardView.Render(amount);
        }
    }
}
