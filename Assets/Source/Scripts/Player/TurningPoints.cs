using System.Collections.Generic;
using UnityEngine;

public class TurningPoints : MonoBehaviour
{
    [SerializeField] private List<TurningPoint> _points;

    public TurningPoint GetPoint(int value)
    {
        return value >= _points.Count ? null : _points[value];
    }
}