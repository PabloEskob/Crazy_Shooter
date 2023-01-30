using Assets.Source.Scripts.Analytics;
using Source.Infrastructure;
using Source.Scripts.StaticData;
using System;
using System.Diagnostics;
using UnityEngine.SceneManagement;

public class StartLevelState : ILevelState
{
    private LevelStateMachine _levelStateMachine;
    private IAnalyticManager _analyticManager;
    private GameConfig _gameConfig;
    private IStaticDataService _staticData;

    public StartLevelState(LevelStateMachine levelStateMachine, IAnalyticManager analyticManager)
    {
        _levelStateMachine = levelStateMachine;
        _analyticManager = analyticManager;
        _staticData = AllServices.Container.Single<IStaticDataService>();
        _gameConfig = _staticData.GetGameConfig();
    }

    private int GetCurrentLevelNumber()
    {
        string currentLevelName = SceneManager.GetActiveScene().name;
        return _gameConfig.GetLevelNumberByName(currentLevelName);
    }

    public void Enter()
    {
        _analyticManager.SendEventOnLevelStart(GetCurrentLevelNumber());
        _levelStateMachine.Enter<SpawnEnemyState>();
    }

    public void Exit() { }

}
