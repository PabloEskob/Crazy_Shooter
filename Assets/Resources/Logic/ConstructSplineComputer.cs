using Dreamteck.Splines;
using UnityEngine;

public class ConstructSplineComputer : MonoBehaviour
{
    [SerializeField] private SplineFollower _splineFollower;

    private SplineComputer _splineComputer;
    private TriggerGroup _triggerGroup;

    public void Construct(SplineComputer splineComputer)
    {
        _splineComputer = splineComputer;
        _splineFollower.spline = _splineComputer;
    }

    public void SetSpeed(float speed) => 
        _splineFollower.followSpeed = speed;

    public TriggerGroup[] GetTriggerGroup()
    {
        var triggerGroup = _splineComputer.triggerGroups;
        return triggerGroup;
    }
}