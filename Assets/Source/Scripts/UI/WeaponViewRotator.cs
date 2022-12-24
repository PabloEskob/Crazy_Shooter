using System;
using System.Collections;
using System.Collections.Generic;
using InfimaGames.LowPolyShooterPack;
using Source.Scripts.Ui;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponViewRotator : MonoBehaviour
{
    [SerializeField] private float _autoRotationSpeed = 0.03f;
    [SerializeField] private float _manualRotationSpeed;
    [SerializeField] private bool _canRotate = false;
    [SerializeField] private float _freezeTime;
    [SerializeField] private List<WeaponPlate> _weaponPlates;
    [SerializeField] private Quaternion _defaultRotation = new Quaternion(0, -90, 0, 0);
    
    private Weapon _currentWeapon;
    private readonly Timer _timer = new Timer();
    private bool isCurrentWeaponNotNull;

    public Weapon CurrentWeapon => _currentWeapon;

    private void Awake() => 
        _currentWeapon = _weaponPlates[0].Weapon;

    private void OnEnable()
    {
        foreach (WeaponPlate weaponPlate in _weaponPlates)
            weaponPlate.WeaponSelected += OnWeaponSelected;

        _timer.Completed += OnTimerCompleted;
    }

    private void OnDisable()
    {
        foreach (WeaponPlate weaponPlate in _weaponPlates)
            weaponPlate.WeaponSelected += OnWeaponSelected;

        _timer.Completed -= OnTimerCompleted;
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

        if (_canRotate)
            _currentWeapon.transform.Rotate(Vector3.up, _autoRotationSpeed);

        if (Input.GetMouseButtonUp(0))
            _timer.Start(_freezeTime);
    }

    private void OnMouseDrag()
    {
        float XAxis = Input.GetAxis("Mouse X") * _manualRotationSpeed;

        SwitchRotationState(false);
        _timer.Stop();

        if (Input.GetAxis("Mouse X") != 0)
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