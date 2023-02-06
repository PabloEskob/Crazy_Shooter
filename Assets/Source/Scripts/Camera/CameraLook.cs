using System.Collections;
using Assets.Source.Scripts.UI;
using Source.Infrastructure;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    public class CameraLook : MonoBehaviour 
    {
        [Header("Settings")] [Tooltip("Sensitivity when looking around.")] [SerializeField]
        private Vector2 _sensitivity = new(1, 1);

        [Tooltip("Minimum and maximum up/down rotation angle the camera can have.")] [SerializeField]
        private Vector2 _yClamp = new(-40, 60);

        [Tooltip("Minimum and maximum left/right rotation angle the player can have.")] [SerializeField]
        private Vector2 _xClamp = new(-240, -120);

        [SerializeField] private bool _allRoundView;
        [SerializeField] private float _speedRotateToFinish;

        private Coroutine _coroutine;
        private CharacterBehaviour _playerCharacter;
        private float _yaw;
        private float _pitch;
        private IStorage _storage;
        private Vector3 _positionToLook;
        private bool _canRotate;


        private void Awake() =>
            _playerCharacter = ServiceLocator.Current.Get<IGameModeService>().GetPlayerCharacter();

        private void Start()
        {
            _storage = AllServices.Container.Single<IStorage>();

            if (_storage.HasKeyFloat(SettingsNames.SensitivityKey))
                SetSensitivity(_storage.GetFloat(SettingsNames.SensitivityKey));
        }

        public void Switch(bool value)
        {
            _canRotate = value;
            StartRoutine();
        }

        public void StopRoutine() =>
            StopCoroutine(_coroutine);

        public void StartRotateToFinish(TurningPoint turningPoint) =>
            _coroutine = StartCoroutine(StartRotateToTarget(turningPoint));

        private IEnumerator StartRotateToTarget(TurningPoint turningPoint)
        {
            while (true)
            {
                UpdatePositionToLookAt(turningPoint);
                yield return new WaitForEndOfFrame();
            }
        }

        private void UpdatePositionToLookAt(TurningPoint finishLevelTurningPoint)
        {
            Vector3 positionDiff = finishLevelTurningPoint.transform.position - transform.position;
            _positionToLook = positionDiff;
            transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
        }

        private void StartRoutine() =>
            _coroutine = StartCoroutine(StartRotate());

        private float ClampAngle(float angle, float from, float to)
        {
            if (angle < 0f)
                angle = 360 + angle;
            return angle > 180f ? Mathf.Max(angle, 360 + from) : Mathf.Min(angle, to);
        }

        private void DisableCamera()
        {
            transform.localRotation =
                Quaternion.Lerp(transform.localRotation, Quaternion.identity, Time.deltaTime);
        }

        private IEnumerator StartRotate()
        {
            while (true)
            {
                if (_canRotate)
                    RotationOfWeaponAndCameras();
                else
                    DisableCamera();

                yield return new WaitForEndOfFrame();
            }
        }

        private void SetSensitivity(float value) =>
            _sensitivity = new Vector2(value, value);

        private void RotationOfWeaponAndCameras()
        {
            Vector2 frameInput = _playerCharacter.IsCursorLocked() ? _playerCharacter.GetInputLook() : default;
            frameInput *= _sensitivity;

            var transformLocalRotation = transform.localRotation;
            Vector3 rot = transformLocalRotation.eulerAngles + new Vector3(-frameInput.y, frameInput.x, 0f);


            if (_allRoundView)
                rot.y = ClampAngle(rot.y, _xClamp.x, _xClamp.y);

            rot.x = ClampAngle(rot.x, _yClamp.x, _yClamp.y);
            transformLocalRotation.eulerAngles = rot;
            transform.localRotation = transformLocalRotation;
        }

        private Quaternion SmoothedRotation(Quaternion rotation, Vector3 positionToLook)
        {
            if (rotation == TargetRotation(positionToLook))
                StopRoutine();

            return Quaternion.Lerp(rotation, TargetRotation(positionToLook), SpeedFactor());
        }

        private Quaternion TargetRotation(Vector3 positionToLook) =>
            Quaternion.LookRotation(positionToLook);

        private float SpeedFactor() =>
            _speedRotateToFinish * Time.deltaTime;

       
    }
}