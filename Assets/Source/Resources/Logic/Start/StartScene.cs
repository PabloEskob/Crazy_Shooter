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
    private LevelAdjustmentTool _levelAdjustmentTool;

    private void OnDisable()
    {
        foreach (var zone in _levelAdjustmentTool._zones)
        {
            zone.OnDisable();
        }
    }

    public void Construct(IGameFactory gameFactory, GameStatusScreen gameStatusScreen
       ,LevelAdjustmentTool levelAdjustmentTool)
    {
        _gameStatusScreen = gameStatusScreen;
        _gameFactory = gameFactory;
        _levelAdjustmentTool = levelAdjustmentTool;

        InitGameWorld();
    }
    
    private void Awake()
    {
        _assetProvider = AllServices.Container.Single<IAssetProvider>();
        _staticDataEnemy = new StaticDataService();
    }
    
    private void InitGameWorld() =>
        _levelAdjustmentTool.Fill(_gameFactory);

    
}