using Assets.Source.Scripts.Analytics;
using Source.Infrastructure;
using Source.Scripts.StaticData;
using System;
using System.Diagnostics;
using UnityEngine.SceneManagement;

public class StartLevelState : ILevelState
{
    private readonly LevelStateMachine _levelStateMachine;
    private readonly IAnalyticManager _analyticManager;
    private readonly GameConfig _gameConfig;
    private readonly IStaticDataService _staticData;
    private readonly StartAlert _alert;

    public StartLevelState(LevelStateMachine levelStateMachine, IAnalyticManager analyticManager, StartAlert startAlert)
    {
        _alert = startAlert;
        _levelStateMachine = levelStateMachine;
        _analyticManager = analyticManager;
        _staticData = AllServices.Container.Single<IStaticDataService>();
        _gameConfig = _staticData.GetGameConfig();
    }

    public void Enter()
    {
        _analyticManager.SendEventOnLevelStart(GetCurrentLevelNumber());
        ChangeState();
    }

    public void Exit()
    {
    }

    private int GetCurrentLevelNumber()
    {
        string currentLevelName = SceneManager.GetActiveScene().name;
        return _gameConfig.GetLevelNumberByName(currentLevelName);
    }

    private void ChangeState()
    {
        switch (_alert.ChooseDialoguePlaceCurrent)
        {
            case StartAlert.ChooseDialoguePlace.End:
                _levelStateMachine.Enter<SpawnEnemyState>();
                break;
            case StartAlert.ChooseDialoguePlace.Start:
            case StartAlert.ChooseDialoguePlace.StartAndEnd:
                _levelStateMachine.Enter<NarrativeState>();
                break;
        }
    }
}