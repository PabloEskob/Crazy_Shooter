﻿using System.Collections;
using Agava.YandexGames;
using Source.Scripts;
using UnityEngine;

namespace Source.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingScreen _loadingScreenPrefab;
        
        private Game _game;

#if UNITY_WEBGL && !UNITY_EDITOR
        private IEnumerator Start()
        {
            yield return YandexGamesSdk.Initialize();
            
            _game = new Game(this,Instantiate(_loadingScreenPrefab));
            _game.StateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }       
#else
        private void Start()
        {
            _game = new Game(this, Instantiate(_loadingScreenPrefab));
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
#endif
    }
}