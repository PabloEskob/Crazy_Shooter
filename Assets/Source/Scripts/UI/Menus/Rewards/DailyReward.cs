using Source.Scripts.Infrastructure.Services.PersistentProgress;
using Source.Scripts.Ui;
using System;
using UnityEngine;

public class DailyReward : MonoBehaviour
{
    [SerializeField] private MainMap _mainMap;
    [SerializeField] private SoftCurrencyHolder _softCurrencyHolder;
    [SerializeField] private int[] _rewards;
    
    private int _currentIndex;
    private DateTime _lastLogin;
    private DateTime _lastRewardDay;
    private IStorage _storage;

    private const string RewardIndexKey = "Reward Index";
    private const string LastLoginDayKey = "LastLoginDay";
    private const string LastRewardDayKey = "LastRewardDay";


    private void Start()
    {
        _storage = _mainMap.Storage;

        if(_storage.HasKeyString(LastRewardDayKey))
            _lastRewardDay = DateTime.Parse(_storage.GetString(LastRewardDayKey));

        if (_storage.HasKeyInt(RewardIndexKey))
            _currentIndex = _storage.GetInt(RewardIndexKey);

        if (_storage.HasKeyString(LastLoginDayKey))
            _lastLogin = DateTime.Parse(_storage.GetString(LastLoginDayKey));

        OnLogIn();
    }

    public void OnLogIn()
    {
        if (_lastRewardDay == DateTime.Today)
            return;

        if (_lastLogin == DateTime.Today)
        {
            _currentIndex = (_currentIndex + 1) % _rewards.Length;
            GiveReward(_rewards[_currentIndex]);
        }
        else
        {
            _currentIndex = 0;
            GiveReward(_rewards[_currentIndex]);
        }

        SetData();
        _storage.Save();
    }

    private void SetData()
    {
        _storage.SetInt(RewardIndexKey, _currentIndex);
        _storage.SetLastLoginDate();
        _storage.SetString(LastLoginDayKey, DateTime.Today.ToString());
        _storage.SetString(LastRewardDayKey, _lastRewardDay.ToString());
    }

    private void GiveReward(int reward)
    {
        _softCurrencyHolder.AddReward(reward);
        _lastRewardDay = DateTime.Today;
    }

}
