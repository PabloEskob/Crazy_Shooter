﻿using System;
using Agava.YandexGames;
using Assets.Source.Scripts.Character;
using Assets.Source.Scripts.UI.RouletteWheel;
using InfimaGames.LowPolyShooterPack;
using Source.Scripts.Data;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
using Source.Scripts.Ui;
using UnityEngine;

namespace Assets.Source.Scripts.UI.Menus.Rewards
{
    public class RewardHandler : MonoBehaviour
    {
        [SerializeField] private AdvertisementButton _softRewardButton;
        [SerializeField] private AdvertisementButton _grenadeRewardButton;
       // [SerializeField] private ProjectContext _projectContext;
        [SerializeField] private CurrencyHolder _currencyHolder;
        [SerializeField] private RouletteDisplay _rouletteDisplay;
        [SerializeField] private GrenadesData _grenadesData;
        [SerializeField] private MainMap _mainMap;
        [SerializeField] private int _softRewardAmount;

        private bool _startedPlayingAds;

        IStorage Storage => _mainMap.Storage;
        

        private void OnEnable()
        {
            _softRewardButton.ButtonClicked += OnSoftButtonClick;
            _grenadeRewardButton.ButtonClicked += OnGrenadeButtonClick;
            _rouletteDisplay.Roulette.Stopped += OnRouletteStopped;
        }

        private void OnDisable()
        {
            _softRewardButton.ButtonClicked -= OnSoftButtonClick;
            _grenadeRewardButton.ButtonClicked -= OnGrenadeButtonClick;
            _rouletteDisplay.Roulette.Stopped -= OnRouletteStopped;
        }

        private void OnSoftButtonClick()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            VideoAd.Show(OnAdvertisementStart, null, OnSoftRewardAdClose);
#endif

#if UNITY_EDITOR
            OnSoftRewardAdClose();
#endif
        }

        private void OnGrenadeButtonClick()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            VideoAd.Show(OnAdvertisementStart, null, OnGrenadeRewardAdClose);
#endif

#if UNITY_EDITOR
            OnGrenadeRewardAdClose();
#endif
        }

        private void OnAdvertisementStart()
        {
            _startedPlayingAds = true;
            ProjectContext.Instance.SetPauseWhenAds(_startedPlayingAds,true);
        }

        private void OnAdvertisementError()
        {
            _startedPlayingAds = false;
            ProjectContext.Instance.SetPauseWhenAds(_startedPlayingAds,false);
        }

        private void OnGrenadeRewardAdClose()
        {
            _startedPlayingAds = false;
            ProjectContext.Instance.SetPauseWhenAds(_startedPlayingAds,false);
            _rouletteDisplay.gameObject.SetActive(true);
        }

        private void OnSoftRewardAdClose()
        {
            _startedPlayingAds = false;
            ProjectContext.Instance.SetPauseWhenAds(_startedPlayingAds,false);
            _currencyHolder.AddSoft(_softRewardAmount);
        }
        
        private void OnRouletteStopped(Reward reward)
        {
            if (reward.Type == RewardType.SoftCurrency)
                _currencyHolder.AddSoft(reward.Quantity);

            if (reward.Type == RewardType.Weapon)
            {
                Weapon weapon = reward.GetWeapon();
                weapon.SetIsCollected();
                weapon.SetIsBought();
                Storage.SetString(weapon.GetName(), weapon.GetData().ToJson());
                Storage.Save();
            }

            if (reward.Type == RewardType.Grenade)
                _grenadesData.TryAddGrenade(reward.Quantity);

            _rouletteDisplay.gameObject.SetActive(false);
        }
    }
}
