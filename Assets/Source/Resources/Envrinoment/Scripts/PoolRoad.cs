using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolRoad : MonoBehaviour
{
    [SerializeField] private List<Road> _roads;
    [SerializeField] private SpawnPointRoad _spawnPoint;

    private void OnEnable()
    {
        foreach (var road in _roads)
        {
            road.OnSwitchedOff += TurnOnAnotherRoad;
        }
    }

    private void OnDisable()
    {
        foreach (var road in _roads)
        {
            road.OnSwitchedOff -= TurnOnAnotherRoad;
        }
    }

    private void TurnOnAnotherRoad()
    {
        var firstRoad = _roads.First(r => r.gameObject.activeSelf == false);
        firstRoad.ReturnToThePlace(_spawnPoint.transform.position);
    }
}