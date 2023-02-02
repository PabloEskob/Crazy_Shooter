using Assets.Source.Scripts.UI.LevelRewards;
using Source.Scripts.Infrastructure.Services;
using Source.Scripts.StaticData;
using System;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScreen : Screen
{
    [SerializeField] LevelTransfer _levelTransfer;
    [SerializeField] RewardDisplay _rewardDisplay;

    GameConfig _gameConfig;

    public event Action<string> Loaded;

    protected override void Start()
    {
        base.Start();
        _gameConfig = _levelTransfer.GameConfig;

        Loaded?.Invoke(_gameConfig.GetLevelReward(_levelTransfer.CurrentLevelNumber).ToString());
    }
}