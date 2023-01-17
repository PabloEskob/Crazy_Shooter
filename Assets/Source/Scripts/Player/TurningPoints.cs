using System.Collections.Generic;
using UnityEngine;

public class TurningPoints : MonoBehaviour
{
    [SerializeField] private List<TurningPoint> _points;
    
    public TurningPoint GetPoint(int value) => 
        _points[value];
}