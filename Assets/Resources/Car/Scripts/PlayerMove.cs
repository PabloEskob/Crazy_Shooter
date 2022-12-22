using Dreamteck.Splines;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private SplineFollower _splineFollower;

    private SplineComputer _splineComputer;

    public void Construct(SplineComputer splineComputer)
    {
        _splineFollower.spline = splineComputer;
        CreateSplineTrigger(splineComputer);
    }

    public void PlayMove()
    {
        _splineFollower.followSpeed = _speed;
    }

    public void PlayMove(InputAction.CallbackContext context)
    {
        _splineFollower.followSpeed = _speed;
    }

    private void CreateSplineTrigger(SplineComputer splineComputer)
    {
        foreach (var triggerGroup in splineComputer.triggerGroups)
        {
            foreach (var splineTrigger in triggerGroup.triggers)
            {
                splineTrigger.AddListener(StopMove);
            }
        }
    }

    private void StopMove(SplineUser arg0)
    {
        _splineFollower.followSpeed = 0;
    }
}