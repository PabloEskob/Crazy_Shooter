using System;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Screen;

namespace InfimaGames.LowPolyShooterPack.Interface
{
    public class Crosshair : Element
    {
        [SerializeField] private float smoothing = 8.0f;
        [SerializeField] private float minimumScale = 0.15f;
        [SerializeField] private AnimationClip _animationClip;
        [SerializeField] private LayerMask _layerMask;

        private Animation _animation;
        private float current = 1.0f;
        private float target = 1.0f;
        private RectTransform rectTransform;
        private Camera _camera;
        private Vector3 _rayStartPosition;
        private RaycastHit _hit;

        protected override void Awake()
        {
            base.Awake();
            rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            _animation = GetComponent<Animation>();
            _camera = Camera.main;
            _rayStartPosition = new Vector3(width / 2, height / 2, 0);
        }

        protected override void Tick()
        {
            FireABeam();

            bool visible = playerCharacter.IsCrosshairVisible();
            target = visible ? 1.0f : 0.0f;
            current = Mathf.Lerp(current, target, Time.deltaTime * smoothing);
            rectTransform.localScale = Vector3.one * current;

            for (var i = 0; i < transform.childCount; i++)
                transform.GetChild(i).gameObject.SetActive(current > minimumScale);
        }

        public void TurnOnAnimation()
        {
            _animation.clip = _animationClip;
            _animation.Play();
        }

        private void FireABeam()
        {
            Ray ray = _camera.ScreenPointToRay(_rayStartPosition);

            if (Physics.Raycast(ray, out _hit, 100f, _layerMask, QueryTriggerInteraction.Ignore))
                SetColorChild(_hit.collider.GetComponentInParent<Enemy>() ? Color.red : Color.white);
            else
                SetColorChild(Color.white);
        }

        private void SetColorChild(Color color)
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                var image = transform.GetChild(i).GetComponent<Image>();
                image.color = color;
            }
        }
    }
}