using System;
using System.Collections.Generic;
using Source.Scripts.Infrastructure.Factory;
using UnityEngine;

public class LaunchRoom : MonoBehaviour
{
    [SerializeField] private List<Room> _rooms;

    public event Action Allowed;
    public event Action EndedRoom;

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
        StartCoroutine(_rooms[0].LaunchingWaves.StartWave(_rooms[0].StartThisRoom, _rooms[0].DelayWave));
    }

    private void StartedNewRoom(int number)
    {
        Allowed?.Invoke();

        var numberRoom = number + 1;

        if (_rooms.Count > numberRoom)
        {
            var startWave = _rooms[numberRoom].LaunchingWaves
                .StartWave(_rooms[numberRoom].StartThisRoom, _rooms[numberRoom].DelayWave);
            StartCoroutine(startWave);
        }
        else
        {
            EndedRoom?.Invoke();
            Debug.Log("тут конец");
        }
    }
}