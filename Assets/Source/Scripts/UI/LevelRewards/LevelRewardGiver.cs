using InfimaGames.LowPolyShooterPack;
using Source.Scripts.Data;
using Source.Scripts.Infrastructure.Services;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
using Source.Scripts.StaticData;
using System;
using UnityEngine;

namespace Assets.Source.Scripts.UI.LevelRewards
{
    public class LevelRewardGiver : MonoBehaviour
    {
        [SerializeField] LevelTransfer _levelTransfer;
        [SerializeField] SoftCurrencyHolder _softCurrencyHolder;

        private GameConfig _gameConfig => _levelTransfer.GameConfig;
        private IStorage _storage => _levelTransfer.Storage;

        private void OnEnable()
        {
            _levelTransfer.LevelCompleted += OnLevelCompleted;
        }

        private void OnDisable()
        {
            _levelTransfer.LevelCompleted -= OnLevelCompleted;
        }

        private void OnLevelCompleted()
        {
            if (_gameConfig.HasSoftReward(_levelTransfer.CurrentLevelNumber))
                _softCurrencyHolder.AddSoft(_gameConfig.GetLevelReward(_levelTransfer.CurrentLevelNumber));

            if (_gameConfig.HasExtraReward(_levelTransfer.CurrentLevelNumber))
            {
                var rewardWeapon = _gameConfig.GetExtraReward(_levelTransfer.CurrentLevelNumber);
                rewardWeapon.SetIsCollected();
                _storage.SetString(rewardWeapon.GetName(), rewardWeapon.GetData().ToJson());
                _storage.Save();
            }
        }
    }
}
