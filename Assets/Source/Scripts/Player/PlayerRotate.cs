using Dreamteck.Splines;
using InfimaGames.LowPolyShooterPack;
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
    private CameraLook _cameraLook;

    private void Awake()
    {
        _splineFollower = GetComponent<SplineFollower>();
        _cameraLook = GetComponentInChildren<CameraLook>();
    }

    private void Start() =>
        _turningPoints = GameObject.FindGameObjectWithTag(TurningPointsTag).GetComponent<TurningPoints>();

    private void Update()
    {
        if (_canRotate)
            RotateToEnemy(_targetVector);

        if (_canReturn)
            RotateToZero(Vector3.zero);
    }

    public void StartRotate()
    {
        _targetVector = SetNewVector(_number);
        _canRotate = true;
        _number++;
    }

    public void RotateReturn()
    {
        _canReturn = true;
        _cameraLook.Switch();
    }

    private void RotateToEnemy(Vector3 target)
    {
        if (_splineFollower.motion.rotationOffset != target)
            Rotate(target);
        else
        {
            _cameraLook.Switch();
            _elapsedTime = 0;
            _canRotate = false;
        }
    }

    private void RotateToZero(Vector3 target)
    {
        if (_splineFollower.motion.rotationOffset != target)
            Rotate(target);
        else
        {
            _elapsedTime = 0;
            _canReturn = false;
        }
    }

    private void Rotate(Vector3 target)
    {
        _elapsedTime += Time.deltaTime;
        float percentageCompleted = _elapsedTime / _speedRotate;
        _splineFollower.motion.rotationOffset =
            Vector3.MoveTowards(_splineFollower.motion.rotationOffset, target, percentageCompleted);
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