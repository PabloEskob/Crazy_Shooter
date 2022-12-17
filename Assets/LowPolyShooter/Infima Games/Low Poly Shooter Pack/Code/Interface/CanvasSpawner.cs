// Copyright 2021, Infima Games. All Rights Reserved.

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

        #region UNITY

        /// <summary>
        /// Awake.
        /// </summary>
        private void Start()
        {
            //Spawn Interface.
#if !UNITY_EDITOR && UNITY_WEBGL
            StartCoroutine(InstantiateRoutine());
#endif

#if UNITY_EDITOR
            Instantiate(_mobileCanvasPrefab);
#endif
            
            //Spawn Quality Settings Menu.
        }

        private IEnumerator InstantiateRoutine()
        {
            Debug.Log("Wait SDK initialization");
            //yield return new WaitWhile(()=>YandexGamesSdk.IsInitialized == false);
            yield return null;
            
            Debug.Log($"SDK initialized = {YandexGamesSdk.IsInitialized}");
            Debug.Log($"Device Type is {Device.Type}");
            Debug.Log($"Device Type must be shown");
            
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
        }

        #endregion
    }
}