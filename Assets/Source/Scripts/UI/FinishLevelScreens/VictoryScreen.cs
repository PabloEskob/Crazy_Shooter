using Source.Scripts.Infrastructure.Services;
using Source.Scripts.StaticData;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScreen : Screen
{
    [SerializeField] LevelTransfer _levelTransfer;
    [SerializeField] private Image _rewardIcon;
    [SerializeField] private Text _rewardText;

    GameConfig _gameConfig;

    protected override void Start()
    {
        base.Start();
        _gameConfig = _levelTransfer.GameConfig;
        SetRewardText();
    }

    private void SetRewardText() => 
        _rewardText.text = _gameConfig.GetLevelReward(_levelTransfer.CurrentLevelNumber).ToString();
}