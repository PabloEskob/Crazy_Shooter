using System;
using Assets.Source.Scripts.UI;
using Source.Infrastructure;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    public class CameraLook : MonoBehaviour
    {
        #region FIELDS SERIALIZED

        [Header("Settings")] [Tooltip("Sensitivity when looking around.")] [SerializeField]
        private Vector2 _sensitivity = new Vector2(1, 1);

        [Tooltip("Minimum and maximum up/down rotation angle the camera can have.")] [SerializeField]
        private Vector2 _yClamp = new Vector2(-40, 60);

        [Tooltip("Minimum and maximum left/right rotation angle the player can have.")] [SerializeField]
        private Vector2 _xClamp = new Vector2(-240, -120);

        [SerializeField] private bool _allRoundView;
        [SerializeField] private float _speedRotateToFinish;

        #endregion

        #region FIELDS

        private CharacterBehaviour _playerCharacter;
        private float _yaw;
        private float _pitch;
        private bool _canRotate = true;
        private IStorage _storage;
        private Vector3 _positionToLook;

        public event Action LookedAtTarget; 

        #endregion

        #region UNITY

        private void Awake() =>
            _playerCharacter = ServiceLocator.Current.Get<IGameModeService>().GetPlayerCharacter();

        private void Start()
        {
            _storage = AllServices.Container.Single<IStorage>();

            if (_storage.HasKeyFloat(SettingsNames.SensitivityKey))
                SetSensitivity(_storage.GetFloat(SettingsNames.SensitivityKey));
        }

        private void Update()
        {
            Vector2 frameInput = _playerCharacter.IsCursorLocked() ? _playerCharacter.GetInputLook() : default;
            frameInput *= _sensitivity;

            var transformLocalRotation = transform.localRotation;
            Vector3 rot = transformLocalRotation.eulerAngles + new Vector3(-frameInput.y, frameInput.x, 0f);

            if (_canRotate)
            {
                if (_allRoundView)
                    rot.y = ClampAngle(rot.y, _xClamp.x, _xClamp.y);

                rot.x = ClampAngle(rot.x, _yClamp.x, _yClamp.y);
                transformLocalRotation.eulerAngles = rot;
                transform.localRotation = transformLocalRotation;
            }
            else
            {
                transform.localRotation =
                    Quaternion.Lerp(transform.localRotation, Quaternion.identity, Time.deltaTime);
            }
        }

        public void Switch(bool value) =>
            _canRotate = value;

        private float ClampAngle(float angle, float from, float to)
        {
            if (angle < 0f)
                angle = 360 + angle;
            return angle > 180f ? Mathf.Max(angle, 360 + from) : Mathf.Min(angle, to);
        }

        #endregion

        #region FUNCTIONS

        private void SetSensitivity(float value) =>
            _sensitivity = new Vector2(value, value);

        #endregion
        
        public void UpdatePositionToLookAt(TurningPoint finishLevelTurningPoint)
        {
            Vector3 positionDiff = finishLevelTurningPoint.transform.position - transform.position;
            _positionToLook = positionDiff;
            transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
        }

        private Quaternion SmoothedRotation(Quaternion rotation, Vector3 positionToLook)
        {
            if (rotation == TargetRotation(positionToLook))
               LookedAtTarget?.Invoke();
            return Quaternion.Lerp(rotation, TargetRotation(positionToLook), SpeedFactor());
        }

        private float SpeedFactor() =>
            _speedRotateToFinish * Time.deltaTime;

        private Quaternion TargetRotation(Vector3 positionToLook) =>
            Quaternion.LookRotation(positionToLook);
    }
}