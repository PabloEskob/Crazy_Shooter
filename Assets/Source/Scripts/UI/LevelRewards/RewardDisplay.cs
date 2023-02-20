using Source.Scripts.Infrastructure.Services;
using Source.Scripts.StaticData;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Scripts.UI.LevelRewards
{
    public class RewardDisplay : MonoBehaviour
    {
        [SerializeField] private LevelTransfer _levelTransfer;
        [SerializeField] private VictoryScreen _victoryScreen;
        [SerializeField] private LevelRewardView _rewardTemplate;
        [SerializeField] private ExtraRewardView _extraRewardTemplate;
        [SerializeField] private Transform _container;

        private GameConfig _gamneConfig => _levelTransfer.GameConfig;
        private int CurrentLevel => _levelTransfer.CurrentLevelNumber;

        private void OnEnable() => _victoryScreen.Loaded += OnLoaded;

        private void OnDisable() => _victoryScreen.Loaded -= OnLoaded;

        private void OnLoaded(string amount)
        {
            if (_gamneConfig.HasSoftReward(CurrentLevel))
                ShowSoftReward(amount);

            if (_gamneConfig.HasExtraReward(CurrentLevel))
                ShowExtraReward(_gamneConfig.GetExtraReward(CurrentLevel).Icon);
        }

        public void ShowSoftReward(string amount)
        {
            LevelRewardView rewardView = Instantiate(_rewardTemplate, _container);
            rewardView.Render(amount);
        }

        public void ShowExtraReward(Sprite image)
        {
            ExtraRewardView extraRewardView = Instantiate(_extraRewardTemplate, _container);
            extraRewardView.Render(image);
        }
    }
}
