using Dreamteck.Splines;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private SplineFollower _splineFollower;

    public void Construct(SplineComputer splineComputer)
    {
       // _splineFollower.spline = splineComputer;
    }

    public void SwitchMove()
    {
        _splineFollower.enabled = _splineFollower.enabled == false;
    }
}