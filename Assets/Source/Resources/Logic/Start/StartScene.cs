using System;
using Source.Infrastructure;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    private GameStatusScreen _gameStatusScreen;
    private StaticDataService _staticDataEnemy;
    private IGameFactory _gameFactory;
    private IAssetProvider _assetProvider;
    private int _number;
    private FinishLevel _finishLevel;
    private LevelAdjustmentTool _levelAdjustmentTool;

    private void OnDisable()
    {
        _finishLevel.OnEndedLevel -= LaunchVictoryScreen;
        
        foreach (var zone in _levelAdjustmentTool._zones)
        {
            zone.OnDisable();
        }
    }

    public void Construct(IGameFactory gameFactory, GameStatusScreen gameStatusScreen,
        FinishLevel finishLevel,LevelAdjustmentTool levelAdjustmentTool)
    {
        _gameStatusScreen = gameStatusScreen;
        _gameFactory = gameFactory;
        _levelAdjustmentTool = levelAdjustmentTool;
        _finishLevel = finishLevel;
        _finishLevel.OnEndedLevel += LaunchVictoryScreen;
        InitGameWorld();
    }

    private void LaunchVictoryScreen() => 
        _gameStatusScreen.PlayerVictory();

    private void Awake()
    {
        _assetProvider = AllServices.Container.Single<IAssetProvider>();
        _staticDataEnemy = new StaticDataService();
    }
    
    private void InitGameWorld() =>
        _levelAdjustmentTool.Fill(_gameFactory);

    
}