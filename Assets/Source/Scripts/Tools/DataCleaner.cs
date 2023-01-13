using Source.Infrastructure;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;


public class DataCleaner : MonoBehaviour
{
    private IStorage _storage;
    //private IGameStateMachine _gameStateMachine;

    private void Awake()
    {
        _storage = AllServices.Container.Single<IStorage>();
        //_gameStateMachine = AllServices.Container.Single<IGameStateMachine>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
            ClearData();

    //    if (Input.GetKeyDown(KeyCode.N))
    //        LoadNextLevel();

    //    if (Input.GetKeyDown(KeyCode.R))
    //        ReloadLevel();
    }

    public void ClearData()
    {
        StartCoroutine(_storage.ClearDataRemote());
        //_gameStateMachine.Enter<LoadProgressState>();
    }

    //public void LoadNextLevel() =>
        //_gameStateMachine.Enter<LoadNextLevelState>();

    //public void ReloadLevel() =>
        //_gameStateMachine.Enter<ReloadState>();
}