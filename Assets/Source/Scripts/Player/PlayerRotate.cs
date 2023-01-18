using System.Collections;
using Dreamteck.Splines;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    private const string TurningPointsTag = "TurningPoints";

    [SerializeField] private float _speedRotate = 0.3f;

    private TurningPoints _turningPoints;
    private int _number;
    private float _currentSpeedRotate;
    private SplineFollower _splineFollower;
    private float _elapsedTime;
    private Vector3 _targetVector;
    private bool _canRotate;
    private bool _canReturn;

    private void Awake() =>
        _splineFollower = GetComponent<SplineFollower>();

    private void Start() =>
        _turningPoints = GameObject.FindGameObjectWithTag(TurningPointsTag).GetComponent<TurningPoints>();

    private void Update()
    {
        if (_canRotate) 
            Rotate(_targetVector);

        if (_canReturn) 
            Rotate(Vector3.zero);
    }

    public void StartRotate()
    {
        _targetVector = SetNewVector(_number);
        _canRotate = true;
        _number++;
    }

    public void RotateReturn() => 
        _canReturn = true;

    private void Rotate(Vector3 target)
    {
        if (_splineFollower.motion.rotationOffset != target)
        {
            _elapsedTime += Time.deltaTime;
            float percentageCompleted = _elapsedTime / _speedRotate;
            _splineFollower.motion.rotationOffset =
                Vector3.MoveTowards(_splineFollower.motion.rotationOffset, target, percentageCompleted);
        }
        else
        {
            _elapsedTime = 0;
            _canRotate = false;
            _canReturn = false;
        }
    }
    
    private Vector3 SetNewVector(int value)
    {
        var turningPoint = _turningPoints.GetPoint(value);

        if (turningPoint != null)
        {
            Vector3 directionToFace = turningPoint.transform.position - transform.position;
            float angleBetween = Vector3.SignedAngle(directionToFace, Vector3.back, Vector3.up);
            var newVector = new Vector3(0, -angleBetween, 0);
            return newVector;
        }

        return _splineFollower.motion.rotationOffset;
    }
    
}