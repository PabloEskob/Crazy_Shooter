using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class Roulette : MonoBehaviour
{
    [SerializeField] private Transform _wheelImage;
    [SerializeField] private int[] _numbers = { 1, 2, 2, 2, 3, 3, 3, 4, 4, 4, 5, 6, 7, 8, 9, 10 };
    [SerializeField] private float _spinSpeed = 10f;
    [SerializeField] private float _spinningTime = 2f;
    [SerializeField] private float _slowDownTime = 2;

    private int _randomNumber;
    private int _numberOfSegments = 10;
    private int _targetSegment;
    private float _segmentDegree;
    private float _appearTime = 1;

    private const float FullCircleDegrees = 360f;
    private const int Offset = 2;
    private const float NormalScale = 1.0f;

    public event UnityAction<int> Stopped;

    private void OnEnable()
    {
        ChangeScale(NormalScale, _appearTime);
    }

    private void OnDisable()
    {
        ChangeScale(0, 0);
    }

    private void Start() => _segmentDegree = FullCircleDegrees / _numberOfSegments;

    public void SpinRoulette()
    {
        _randomNumber = _numbers[Random.Range(0, _numbers.Length)];
        _targetSegment = _randomNumber - 1;

        _wheelImage.DORotate(new Vector3(0f, 0f, FullCircleDegrees * _spinSpeed), _spinningTime, RotateMode.FastBeyond360)
                 .OnComplete(SlowdownRoulette);
    }

    private void SlowdownRoulette()
    {
        float targetRotationZ = _segmentDegree * _targetSegment + (_segmentDegree / Offset);
        float difference = Mathf.Abs(_wheelImage.rotation.eulerAngles.z - targetRotationZ);
        float time = difference / _slowDownTime;

        _wheelImage.DORotate(new Vector3(0f, 0f, targetRotationZ), time, RotateMode.FastBeyond360)
                 .SetEase(Ease.OutQuad)
                 .OnComplete(OnStopRoulette);
    }

    private void ChangeScale(float targetScale, float time) => transform.DOScale(targetScale, time).OnComplete(OnScaleComplete);

    private void OnScaleComplete()
    {
        if (transform.localScale != Vector3.zero)
            SpinRoulette();
    }

    private void OnStopRoulette() => Stopped?.Invoke(_randomNumber);
}