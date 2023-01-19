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

        #endregion

        #region FIELDS

        private CharacterBehaviour _playerCharacter;
        private Quaternion _rotationCharacter;
        private float _yaw;
        private float _pitch;
        private bool _canRotate = true;
        private IStorage _storage;

        #endregion

        #region UNITY

        private void Awake() =>
            _playerCharacter = ServiceLocator.Current.Get<IGameModeService>().GetPlayerCharacter();

        private void Start()
        {
            _rotationCharacter = _playerCharacter.transform.localRotation;
            _storage = AllServices.Container.Single<IStorage>();

            if (_storage.HasKeyFloat(SettingsNames.SensitivityKey))
                SetSensitivity(_storage.GetFloat(SettingsNames.SensitivityKey));
        }

        private void LateUpdate()
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
                    Quaternion.Lerp(transform.localRotation, new Quaternion(0, 0, 0, 1f), Time.deltaTime);
            }
        }

        public void Switch(bool value) => 
            _canRotate = value;

        private float ClampAngle(float angle, float from, float to)
        {
            if (angle < 0f) angle = 360 + angle;
            return angle > 180f ? Mathf.Max(angle, 360 + from) : Mathf.Min(angle, to);
        }

        private void TurnUp(Vector2 frameInput)
        {
            Quaternion rotationPitch = Quaternion.Euler(-frameInput.y, 0.0f, 0.0f);
            Quaternion localRotation = transform.localRotation;
            localRotation *= rotationPitch;

            localRotation = Clamp(localRotation);
            transform.localRotation = localRotation;
        }

        #endregion

        #region FUNCTIONS

        private Quaternion Clamp(Quaternion rotation)
        {
            rotation.x /= rotation.w;
            rotation.y /= rotation.w;
            rotation.z /= rotation.w;
            rotation.w = 1.0f;

            float pitch = 2.0f * Mathf.Rad2Deg * Mathf.Atan(rotation.y);
            pitch = Mathf.Clamp(pitch, _xClamp.x, _xClamp.y);
            rotation.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * pitch);

            return rotation;
        }

        private void SetSensitivity(float value) =>
            _sensitivity = new Vector2(value, value);

        #endregion
    }
}