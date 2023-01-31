using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LevelAdjustmentTool : MonoBehaviour
{
    public List<Zone> _zones;
    public LevelCategory _typeLevelCategory = LevelCategory.Company;

    private int _countZone;
    private Zone _zone;
    private int _numberRoom;
    private GameObject _container;
    public int Number => _zones.Count;

    private void Start()
    {
        foreach (var zone in _zones)
            zone.Start();
    }

    public void Fill(IGameFactory gameFactory)
    {
        foreach (var zone in _zones) 
            zone.FillInEnemySpawner(gameFactory);
    }

    public Zone GetRoom() =>
        _zones.Count > _numberRoom ? _zones[_numberRoom] : null;

    public void StartRoom(int value)
    {
        _numberRoom = value;
        StartNewRoom();
    }

    public void AddZone()
    {
        _zones.Add(new Zone(_countZone));
        _countZone++;
    }

    public void RemoveAll()
    {
        foreach (var zone in _zones)
        {
            zone.DeleteAllSpawners();
        }

        _zones.Clear();
        _countZone = 0;
    }

    public void DeleteZone()
    {
        if (_zones.Count > 0)
        {
            _zones[^1].DeleteAllSpawners();
            _zones.RemoveAt(_zones.Count - 1);
            _countZone--;
        }
    }

    public void CreateEnemySpawner(int numberZone)
    {
        _zone = _zones.Find(z => z.Number == numberZone);
        _zone.CreateEnemySpawner(transform);
    }

    public void DeleteEnemySpawner(int numberZone)
    {
        _zone = _zones.Find(z => z.Number == numberZone);
        _zone.DeleteLastEnemySpawner();
    }

    private void StartNewRoom()
    {
        var startWave = _zones[_numberRoom].LaunchingWaves;
        startWave.StartWave();
    }

    public enum LevelCategory
    {
        Company,
        Survival
    }
}