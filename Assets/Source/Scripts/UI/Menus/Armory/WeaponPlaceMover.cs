using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Assets.Source.Scripts.UI.Menus.Armory
{
    public class WeaponPlaceMover : MonoBehaviour
    {
        [SerializeField] private float _xOffset = 360;
        [SerializeField] private float _yOffset = 78;
        [SerializeField] private float _moveSpeed;

        private Vector3 _zoomedPosition;

        public float MinScale { get; private set; } = 1f;
        public float MaxScale { get; private set; } = 1.5f;
        public Vector3 DefaultPosition { get; private set; }
        public Vector3 ZoomedPosition => _zoomedPosition;

        private void Awake()
        {
            DefaultPosition = transform.localPosition;
            _zoomedPosition = new Vector3(DefaultPosition.x - _xOffset, DefaultPosition.y + _yOffset, DefaultPosition.z);
        }

        public void MoveTo(Vector3 newPosition, float scale)
        {
            transform.DOLocalMove(newPosition, _moveSpeed);
            transform.DOScale(scale, _moveSpeed);
        }
    }
}
