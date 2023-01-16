using System.Collections.Generic;
using UnityEngine;

public class TurningPoints : MonoBehaviour
{
    [SerializeField] private List<GameObject> _points;
    
    public GameObject GetPoint(int value)
    {
        Debug.Log(value);
        return _points[value];
    }
}