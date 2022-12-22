﻿// Copyright 2021, Infima Games. All Rights Reserved.

using System;
using System.Collections;
using Agava.YandexGames;
using UnityEngine;
using DeviceType = Agava.YandexGames.DeviceType;

namespace InfimaGames.LowPolyShooterPack.Interface
{
    /// <summary>
    /// Player Interface.
    /// </summary>
    public class CanvasSpawner : MonoBehaviour
    {
        #region FIELDS SERIALIZED

        [Header("Settings")]
        [Tooltip("Canvas prefab spawned at start. Displays the player's user interface.")]
        [SerializeField]
        private GameObject _mobileCanvasPrefab;
        [SerializeField]
        private GameObject _pCCanvasPrefab;
        
        [Tooltip("Quality settings menu prefab spawned at start. Used for switching between different quality settings in-game.")]
        [SerializeField]
        private GameObject qualitySettingsPrefab;

        #endregion

        public event Action Spawned;
        
        #region UNITY

        /// <summary>
        /// Awake.
        /// </summary>
        private void Awake()
        {
            Debug.Log($"InitCanvas {DateTime.Now}");
            //Spawn Interface.
#if !UNITY_EDITOR && UNITY_WEBGL
            StartCoroutine(InstantiateRoutine());
#endif

#if UNITY_EDITOR
            Instantiate(_pCCanvasPrefab);
            Instantiate(qualitySettingsPrefab);
#endif
            //Spawn Quality Settings Menu.
        }

        private IEnumerator InstantiateRoutine()
        {
            yield return YandexGamesSdk.Initialize();
            
            switch (Device.Type)
            {
                case DeviceType.Desktop:
                    Instantiate(_pCCanvasPrefab);
                    break;
                case DeviceType.Mobile:
                    Instantiate(_mobileCanvasPrefab);
                    break;
                case DeviceType.Tablet:
                    Instantiate(_mobileCanvasPrefab);
                    break;
                default:
                    Instantiate(_pCCanvasPrefab);
                    break;
                        
            }

            Instantiate(qualitySettingsPrefab);
            Spawned?.Invoke();
        }

        #endregion
    }
}