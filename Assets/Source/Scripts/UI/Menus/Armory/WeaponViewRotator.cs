using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using InfimaGames.LowPolyShooterPack;
using Source.Scripts.Ui;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponViewRotator : MonoBehaviour
{
    [SerializeField] private WeaponPlatesView _platesView;
    [SerializeField] private float _autoRotationSpeed = 0.03f;
    [SerializeField] private float _manualRotationSpeed;
    [SerializeField] private bool _canRotate = false;
    [SerializeField] private float _freezeTime;
    [SerializeField] private Quaternion _defaultRotation = new Quaternion(0, -90, 0, 0);
    
    private List<WeaponPlate> _weaponPlates;
    private Weapon _currentWeapon;
    private readonly Timer _timer = new Timer();
    private bool isCurrentWeaponNotNull;
    
    private const string MouseXAxis = "Mouse X";

    public Weapon CurrentWeapon => _currentWeapon;

    private void OnEnable()
    {
        _weaponPlates = _platesView.Plates.ToList();
        _currentWeapon = _weaponPlates[0].Weapon;
        _currentWeapon.transform.rotation = _defaultRotation;

        foreach (WeaponPlate weaponPlate in _weaponPlates)
            weaponPlate.WeaponSelected += OnWeaponSelected;

        _timer.Completed += OnTimerCompleted;
    }

    private void OnDisable()
    {
        foreach (WeaponPlate weaponPlate in _weaponPlates)
            weaponPlate.WeaponSelected -= OnWeaponSelected;

        _timer.Completed -= OnTimerCompleted;
    }

    private void OnInitialized(List<WeaponPlate> weaponPlates)
    {
        Debug.Log(weaponPlates.Count);
    }

    private void OnWeaponSelected(WeaponPlate plate, Weapon weapon)
    {
        SwitchRotationState(false);
        _timer.Stop();
        SetCurrentWeapon(weapon);
        RestRotation(weapon);
        _timer.Start(_freezeTime);
    }

    private void Start() => 
        _timer.Start(_freezeTime);

    private void Update()
    {
        _timer.Tick(Time.deltaTime);

        if (_canRotate && _weaponPlates.Count > 0)
            _currentWeapon.transform.Rotate(Vector3.up, _autoRotationSpeed);

        if (Input.GetMouseButtonUp(0))
            _timer.Start(_freezeTime);
    }

    private void OnMouseDrag()
    {
        float XAxis = Input.GetAxis(MouseXAxis) * _manualRotationSpeed;

        SwitchRotationState(false);
        _timer.Stop();

        if (Input.GetAxis(MouseXAxis) != 0)
            _currentWeapon.transform.Rotate(Vector3.up, XAxis);
    }

    private void RestRotation(Weapon weapon)
    {
        weapon.transform.rotation = _defaultRotation;
    }

    private void OnTimerCompleted() => 
        SwitchRotationState(true);

    private void SwitchRotationState(bool value) =>
        _canRotate = value;

    public void SetCurrentWeapon(Weapon weapon) => 
        _currentWeapon = weapon;
}