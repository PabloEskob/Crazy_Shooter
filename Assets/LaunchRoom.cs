using System;
using System.Collections.Generic;
using UnityEngine;

public class LaunchRoom : MonoBehaviour
{
    [SerializeField] private List<Room> _rooms;
    [SerializeField] private float _startFirstWave;
    
    public event Action Allowed;

    private void OnDisable()
    {
        foreach (var room in _rooms)
            room.StartedNewRoom -= StartedNewRoom;
    }

    public void Fill(GameFactory gameFactory)
    {
        for (var i = 0; i < _rooms.Count; i++)
        {
            var room = _rooms[i];
            room.FillInEnemySpawner(gameFactory);
            room.Number = i;
            room.StartedNewRoom += StartedNewRoom;
        }
    }

    public void StartFirstRoom()
    {
        StartCoroutine(_rooms[0].LaunchingWaves.StartWave(_startFirstWave));
    }

    private void StartedNewRoom(int number)
    {
        Allowed?.Invoke();
        
        if (_rooms.Count > number + 1)
        {
            var startWave = _rooms[number + 1].LaunchingWaves.StartWave(_startFirstWave);
            StartCoroutine(startWave);
        }
    }
}