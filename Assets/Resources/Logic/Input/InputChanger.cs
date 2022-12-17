using System;
using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using InfimaGames.LowPolyShooterPack.Interface;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using DeviceType = Agava.YandexGames.DeviceType;


public class InputChanger : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private CanvasSpawner _canvasSpawner;
    
    private const string MobileActionMapName = "MobileActionMap";
    private const string DesktopActionMapName = "DesktopActionMap";

    private void Awake() => 
        _playerInput.SwitchCurrentActionMap(DesktopActionMapName);

    private void OnEnable() => 
        _canvasSpawner.Spawned += OnSpawned;

    private void OnDisable() => 
        _canvasSpawner.Spawned -= OnSpawned;

    private void OnSpawned() => 
        Switch();

    private void Switch()
    {
        switch (Device.Type)
        {
            case DeviceType.Desktop:
                _playerInput.SwitchCurrentActionMap(DesktopActionMapName);
                break;
            case DeviceType.Mobile:
                _playerInput.SwitchCurrentActionMap(MobileActionMapName);
                break;
            case DeviceType.Tablet:
                _playerInput.SwitchCurrentActionMap(MobileActionMapName);
                break;
            default:
                _playerInput.SwitchCurrentActionMap(DesktopActionMapName);
                break;
        }
    }
}
