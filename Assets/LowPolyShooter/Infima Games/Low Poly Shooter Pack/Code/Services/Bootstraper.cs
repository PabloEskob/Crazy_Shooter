﻿// Copyright 2021, Infima Games. All Rights Reserved.

using System;
using Agava.YandexGames;
using UnityEngine;
using Object = UnityEngine.Object;

namespace InfimaGames.LowPolyShooterPack
{
    /// <summary>
    /// Bootstraper.
    /// </summary>
    public static class Bootstraper
    {
        /// <summary>
        /// Initialize.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            //if(!YandexGamesSdk.IsInitialized)
                
            
            Debug.Log($"InitBootstrapper {DateTime.Now}");
            //Initialize default service locator.
            ServiceLocator.Initialize();
            
            //Game Mode Service.
            ServiceLocator.Current.Register<IGameModeService>(new GameModeService());
            
            #region Sound Manager Service

            //Create an object for the sound manager, and add the component!
            var soundManagerObject = new GameObject("Sound Manager");
            var soundManagerService = soundManagerObject.AddComponent<AudioManagerService>();
            
            //Make sure that we never destroy our SoundManager. We need it in other scenes too!
            Object.DontDestroyOnLoad(soundManagerObject);
            
            //Register the sound manager service!
            ServiceLocator.Current.Register<IAudioManagerService>(soundManagerService);

            #endregion
        }
    }
}