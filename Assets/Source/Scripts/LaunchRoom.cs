using System.Collections.Generic;
using UnityEngine;

public class LaunchRoom : MonoBehaviour
{
    [SerializeField] private List<Room> _rooms;
    
    private int _numberRoom;
    public int Number => _rooms.Count;
    
    public void Fill(IGameFactory gameFactory)
    {
        for (var i = 0; i < _rooms.Count; i++)
        {
            var room = _rooms[i];
            room.FillInEnemySpawner(gameFactory);
            room.Number = i;
        }
    }

    public Room GetRoom() =>
        _rooms.Count > _numberRoom ? _rooms[_numberRoom] : null;

    public void StartRoom(int value)
    {
        _numberRoom = value;
        StartNewRoom();
    }

    private void StartNewRoom()
    {
        var startWave = _rooms[_numberRoom].LaunchingWaves
            .StartWave(_rooms[_numberRoom].StartThisRoom, _rooms[_numberRoom].DelayWave);
        StartCoroutine(startWave);
    }
}