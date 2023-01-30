using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LevelAdjustmentTool : MonoBehaviour
{
    public List<Zone> _zones;
    public LevelCategory _typeLevelCategory = LevelCategory.Company;
    public int _countZone;

    public enum LevelCategory
    {
        Company,
        Survival
    }

    private Zone _zone;

    public void AddZone()
    {
        _zones.Add(new Zone(_countZone));
        _countZone++;
    }

    public void DeleteZone()
    {
        if (_zones.Count > 0)
        {
            _zones.RemoveAt(_zones.Count - 1);
            _countZone--;
        }
    }

    public void RemoveAll()
    {
        _zones.Clear();
        _countZone = 0;
    }
    
    public void CreateEnemySpawner(int numberZone)
    {
        _zone = _zones.Find(z => z.Number == numberZone);
        _zone.CreateEnemySpawner();
    }

    public void DeleteAllEnemySpawner(int numberZone)
    {
        _zone = _zones.Find(z => z.Number == numberZone);
        _zone.ClearEnemySpawnerListList();
    }
}

[System.Serializable]
public class Zone
{
    public int Number { get; set; }

    public string Name
    {
        get => "Zone";
        set { }
    }

    public int CountEnemySpawners
    {
        get => _enemySpawners.Count;
        set { }
    }

    private List<EnemySpawner> _enemySpawners;
    private int _numberEnemySpawner;

    public Zone(int number)
    {
        Number = number;

        _numberEnemySpawner = 0;
        _enemySpawners = new List<EnemySpawner>();
    }

    public void CreateEnemySpawner()
    {
        var prefabEnemySpawner = Resources.Load(AssetPath.PathSpawner);
        var gameObject = (GameObject)Object.Instantiate(prefabEnemySpawner);
        var enemySpawner = gameObject.GetComponent<EnemySpawner>();
        enemySpawner.SetNumber(_numberEnemySpawner);
        enemySpawner.name = $"EnemySpawner{Number}-{_numberEnemySpawner}";
        _enemySpawners.Add(enemySpawner.GetComponent<EnemySpawner>());
        _numberEnemySpawner++;
    }

    public void ClearEnemySpawnerListList()
    {
        foreach (var enemySpawner in _enemySpawners)
        {
           Object.DestroyImmediate(enemySpawner.gameObject); 
        }
        
        _enemySpawners.Clear();
        _numberEnemySpawner = 0;
    }
}