using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using Assets.Source.Scripts.UI.Menus.Rewards;
using Source.Scripts.Ui;
using Source.Scripts.Infrastructure.Services.PersistentProgress;

namespace Assets.Source.Scripts.UI.RouletteWheel
{
    public class Roulette : MonoBehaviour
    {
        [SerializeField] private MainMap _mainMap;
        [SerializeField] private Transform _wheelImage;
        [SerializeField] private int[] _numbers = { 1, 2, 2, 2, 3, 3, 3, 4, 4, 4, 5, 6, 7, 8, 9, 10 };
        [SerializeField] private float _spinSpeed = 10f;
        [SerializeField] private float _spinningTime = 2f;
        [SerializeField] private float _slowDownTime = 2;
        [SerializeField] private float _delta = 0.2f;
        [SerializeField] private WheelSegment[] _segments = new WheelSegment[10];

        private int _spinCount;
        private int _randomNumber;
        private int _numberOfSegments = 10;
        private int _targetSegment;
        private float _segmentDegree;
        private float _appearTime = 1;

        private const float FullCircleDegrees = 360f;
        private const int Offset = 2;
        private const float NormalScale = 1.0f;
        private const string SpinCountKey = "rouletteSpinCount";

        private IStorage Storage => _mainMap.Storage;

        public event UnityAction<Reward> Stopped;

        private void Awake() => ChangeScale(0, 0);

        private void Start()
        {
            _segmentDegree = FullCircleDegrees / _numberOfSegments;

            if (_mainMap != null && Storage.HasKeyInt(SpinCountKey))
                Load();
        }

        public void SpinRoulette()
        {
            _spinCount++;

            _randomNumber = GetRundomNumber();
            _targetSegment = _randomNumber - 1;

            for (int i = 0; i < _segments.Length; i++)
            {
                WheelSegment segment = _segments[i];

                if (!segment.IsRandom && segment.SpinsToGet == _spinCount)
                    _targetSegment = i;
            }


            //_wheelImage.DORotate(new Vector3(0f, 0f, FullCircleDegrees * _spinSpeed), _spinningTime - _delta, RotateMode.FastBeyond360)
            //         .OnComplete(SlowdownRoulette);

            Sequence spinSequence = DOTween.Sequence();
            spinSequence.Append(_wheelImage.DORotate(new Vector3(0f, 0f, FullCircleDegrees * _spinSpeed), _spinningTime, RotateMode.FastBeyond360));
            spinSequence.AppendInterval(_delta);

            float targetRotationZ = _segmentDegree * _targetSegment + _segmentDegree / Offset;
            float difference = Mathf.DeltaAngle(_wheelImage.rotation.eulerAngles.z, targetRotationZ);
            float time = difference / _slowDownTime;

            spinSequence.Append(_wheelImage.DORotate(new Vector3(0f, 0f, targetRotationZ), _slowDownTime, RotateMode.FastBeyond360).SetEase(Ease.OutQuad));
            spinSequence.OnComplete(OnStopRoulette);
        }

        private int GetRundomNumber()
        {
            return _numbers[Random.Range(0, _numbers.Length)];
        }

        //private void SlowdownRoulette()
        //{
        //    float targetRotationZ = _segmentDegree * _targetSegment + _segmentDegree / Offset;
        //    float difference = Mathf.Abs(_wheelImage.rotation.eulerAngles.z - targetRotationZ);
        //    float time = difference / _slowDownTime;

        //    _wheelImage.DORotate(new Vector3(0f, 0f, targetRotationZ), time, RotateMode.FastBeyond360)
        //             .SetEase(Ease.OutQuad)
        //             .OnComplete(OnStopRoulette);
        //}

        private void ChangeScale(float targetScale, float time) => transform.DOScale(targetScale, time).OnComplete(OnScaleComplete);

        private void OnScaleComplete()
        {
            if (transform.localScale != Vector3.zero)
                SpinRoulette();
        }

        private void OnStopRoulette()
        {
            Stopped?.Invoke(_segments[_targetSegment].Reward);
            Save();
        }

        public void Disappear() => ChangeScale(0, 0);

        public void Appear() => ChangeScale(NormalScale, _appearTime);

        private void Save()
        {
            Storage.SetInt(SpinCountKey, _spinCount);
            Storage.Save();
        }

        public void Load() => _spinCount = Storage.GetInt(SpinCountKey);
    }
}