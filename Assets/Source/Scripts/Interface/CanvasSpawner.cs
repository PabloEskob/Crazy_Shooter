// Copyright 2021, Infima Games. All Rights Reserved.

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

        [SerializeField] private GameObject _pCCanvasPrefab;

        [Tooltip(
            "Quality settings menu prefab spawned at start. Used for switching between different quality settings in-game.")]
        [SerializeField]
        private GameObject qualitySettingsPrefab;

        public GameObject CurrentCanvas { get; set; }

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
           // CurrentCanvas = Instantiate(_pCCanvasPrefab);
             CurrentCanvas = Instantiate(_mobileCanvasPrefab);
            // Instantiate(qualitySettingsPrefab);
#endif
            //Spawn Quality Settings Menu.
        }

        private IEnumerator InstantiateRoutine()
        {
            yield return YandexGamesSdk.Initialize();

            switch (Device.Type)
            {
                case DeviceType.Desktop:
                    CurrentCanvas = Instantiate(_pCCanvasPrefab);
                    break;
                case DeviceType.Mobile:
                    CurrentCanvas = Instantiate(_mobileCanvasPrefab);
                    break;
                case DeviceType.Tablet:
                    CurrentCanvas = Instantiate(_mobileCanvasPrefab);
                    break;
                default:
                    CurrentCanvas = Instantiate(_pCCanvasPrefab);
                    break;
            }

            //  Instantiate(qualitySettingsPrefab);
            Spawned?.Invoke();
        }

        #endregion
    }
}