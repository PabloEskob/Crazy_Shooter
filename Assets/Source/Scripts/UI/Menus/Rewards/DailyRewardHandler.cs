using Assets.Source.Scripts.Character;
using Assets.Source.Scripts.UI.Menus.Rewards;
using InfimaGames.LowPolyShooterPack;
using Source.Scripts.Data;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
using Source.Scripts.Ui;
using System;
using UnityEngine;

public class DailyRewardHandler : MonoBehaviour
{
    [SerializeField] private MainMap _mainMap;
    [SerializeField] private SoftCurrencyHolder _softCurrencyHolder;
    [SerializeField] private GrenadesData _grenadesData;
    [SerializeField] private DailyRewardDisplay[] _rewardsDisplay;

    private DateTime _startDate;
    private DateTime _currentDate = DateTime.Today + TimeSpan.FromDays(DayTest);
    private DateTime _lastLogin;
    private DateTime _lastRewardDay;
    private IStorage _storage;

    private const double DayTest = 0;
    private const string RewardIndexKey = "RewardIndex";
    private const string StartDayKey = "StartDay";
    private const string LastLoginDayKey = "LastLoginDay";
    private const string LastRewardDayKey = "LastRewardDay";

    public int CurrentIndex { get; private set; }

    private void OnEnable()
    {
        foreach (var reward in _rewardsDisplay)
            reward.GetRewardButton.onClick.AddListener(OnGetRewardButtonClick);
    }

    private void OnDisable()
    {
        foreach (var reward in _rewardsDisplay)
            reward.GetRewardButton.onClick.RemoveListener(OnGetRewardButtonClick);
    }

    public void Load()
    {
        _storage = _mainMap.Storage;

        if (_storage.HasKeyInt(RewardIndexKey))
            CurrentIndex = _storage.GetInt(RewardIndexKey);

        if (_storage.HasKeyString(LastRewardDayKey))
            _lastRewardDay = DateTime.Parse(_storage.GetString(LastRewardDayKey));

        if (_storage.HasKeyString(LastLoginDayKey))
            _lastLogin = DateTime.Parse(_storage.GetString(LastLoginDayKey));

        if (_storage.HasKeyString(StartDayKey))
            _startDate = DateTime.Parse(_storage.GetString(StartDayKey));
    }

    public void OnGetRewardButtonClick()
    {
        if (CheckDate())
            return;

        if (_lastLogin == _currentDate - TimeSpan.FromDays(1))
        {
            CurrentIndex = (CurrentIndex + 1) % _rewardsDisplay.Length;
            GiveReward(_rewardsDisplay[CurrentIndex].Reward);

        }
        else
        {
            CurrentIndex = 0;
            GiveReward(_rewardsDisplay[CurrentIndex].Reward);
            _startDate = _currentDate;
        }

        _rewardsDisplay[CurrentIndex].OnRewardGet();
        SetData();
        _storage.Save();
    }

    private void SetData()
    {
        _lastLogin = _currentDate;
        _storage.SetInt(RewardIndexKey, CurrentIndex);
        _storage.SetString(StartDayKey, _startDate.ToString());
        _storage.SetString(LastLoginDayKey, _lastLogin.ToString());
        _storage.SetString(LastRewardDayKey, _lastRewardDay.ToString());
    }

    private void GiveReward(Reward reward)
    {
        if(reward.Type == RewardType.SoftCurrency)
        _softCurrencyHolder.AddSoft(reward.Quantity);

        if(reward.Type == RewardType.Grenade)
            _grenadesData.TryAddGrenade(reward.Quantity);

        if(reward.Type == RewardType.Weapon)
        {
            Weapon weapon = reward.GetWeapon();
            weapon.SetIsCollected();
            weapon.SetIsBought();
            _storage.SetString(weapon.GetName(), weapon.GetData().ToJson());
            _storage.Save();
        }

        _lastRewardDay = _currentDate;
    }

    public int GetDay()
    {
        TimeSpan difference = _currentDate - _startDate;
        TimeSpan difference2 = _currentDate - _lastRewardDay;

        if (difference.Days > 6 || difference2.Days > 1)
            return 0;
        else
            return difference.Days;
    }

    public bool CheckDate() => _lastRewardDay == _currentDate;

}
