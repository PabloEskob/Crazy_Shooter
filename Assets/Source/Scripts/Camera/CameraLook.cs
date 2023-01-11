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

        #endregion

        #region FIELDS

        private CharacterBehaviour _playerCharacter;
        private Quaternion _rotationCharacter;
        private float _yaw;
        private float _pitch;

        #endregion

        #region UNITY

        private void Awake() =>
            _playerCharacter = ServiceLocator.Current.Get<IGameModeService>().GetPlayerCharacter();

        private void Start() =>
            _rotationCharacter = _playerCharacter.transform.localRotation;

        private void LateUpdate()
        {
            Vector2 frameInput = _playerCharacter.IsCursorLocked() ? _playerCharacter.GetInputLook() : default;
            frameInput *= _sensitivity;

            _yaw += frameInput.x;
            _pitch -= frameInput.y;

            _yaw = Mathf.Clamp(_yaw, _xClamp.x, _xClamp.y);
            _pitch = Mathf.Clamp(_pitch, _yClamp.x, _yClamp.y);

            transform.eulerAngles = new Vector3(_pitch, _yaw, 0.0f);
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

            float pitch = 2.0f * Mathf.Rad2Deg * Mathf.Atan(rotation.x);
            pitch = Mathf.Clamp(pitch, _yClamp.x, _yClamp.y);
            rotation.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * pitch);

            return rotation;
        }

        #endregion
    }
}