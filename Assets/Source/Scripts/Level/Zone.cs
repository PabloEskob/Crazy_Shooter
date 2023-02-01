using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class Zone
{
    public int Number;
    public int NumberEnemySpawner;

    public List<EnemySpawner> _enemySpawners;
    public List<TurningPoint> _turningPoints;

    private LaunchingWaves _launchingWaves;
    private int _count;
    private EnemySpawner _enemySpawner;
    private EnemySpawner _nextEnemySpawner;

    public LaunchingWaves LaunchingWaves => _launchingWaves;

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

    public event Action OnRoomCleared;
    public event Action OnNextWave;

    public Zone(int number)
    {
        Number = number;
        NumberEnemySpawner = 0;
        _enemySpawners = new List<EnemySpawner>();
        _turningPoints = new List<TurningPoint>();
    }

    public void DeleteLastEnemySpawner()
    {
        if (_enemySpawners != null && _enemySpawners.Count != 0)
        {
            Object.DestroyImmediate(DeleteTurningPoints(^1).gameObject);
            Object.DestroyImmediate(DeleteEnemySpawners(^1).gameObject);
            _turningPoints.RemoveAt(_turningPoints.Count - 1);
            _enemySpawners.RemoveAt(_enemySpawners.Count - 1);
            NumberEnemySpawner--;
        }
    }

    public void DeleteAllSpawners()
    {
        if (_enemySpawners != null && _turningPoints != null)
        {
            for (int i = 0; i < _enemySpawners.Count; i++)
            {
                Object.DestroyImmediate(DeleteTurningPoints(i).gameObject);
                Object.DestroyImmediate(DeleteEnemySpawners(i).gameObject);
            }

            _turningPoints.Clear();
            _enemySpawners.Clear();
            NumberEnemySpawner = 0;
        }
    }

    public void OnDisable()
    {
        foreach (var enemySpawner in _enemySpawners)
            enemySpawner.OnClearedSpawner -= _launchingWaves.TurnOnSpawn;

        _launchingWaves.OnNextWave -= NextWave;
        _launchingWaves.OnEnded -= LaunchingWavesOnEnded;
    }

    public void Start()
    {
        _launchingWaves = new LaunchingWaves(_enemySpawners);

        for (var index = 0; index < _enemySpawners.Count; index++)
        {
            var enemySpawner = _enemySpawners[index];
            enemySpawner.OnClearedSpawner += _launchingWaves.TurnOnSpawn;
            _enemySpawners[index].Number = index;
        }

        _launchingWaves.OnNextWave += NextWave;
        _launchingWaves.OnEnded += LaunchingWavesOnEnded;
    }

    public TurningPoint GetTurningPoint()
    {
        var number = _count;

        if (_turningPoints[number] == null)
            return null;
        if (number==0)
        {
            _count++;
            return _turningPoints[number];
        }

        number = _nextEnemySpawner.Number;
        return _turningPoints[number];
    }

    public void FillInEnemySpawner(IGameFactory gameFactory)
    {
        foreach (var enemySpawner in _enemySpawners)
            enemySpawner.Construct(gameFactory);
    }

    public void CreateEnemySpawner(Transform parent)
    {
        var instantiate = Instantiate(AssetPath.PathSpawner, parent);
        _enemySpawner = instantiate.GetComponent<EnemySpawner>();
        _enemySpawner.SetNumber(NumberEnemySpawner);
        _enemySpawner.name = $"EnemySpawner {Number}-{NumberEnemySpawner}";
        _enemySpawners.Add(_enemySpawner);
        CreateTurningPoint();

        NumberEnemySpawner++;
    }

    private void LaunchingWavesOnEnded() =>
        OnRoomCleared?.Invoke();

    private void NextWave(EnemySpawner enemySpawner)
    {
        _nextEnemySpawner = enemySpawner;
        OnNextWave?.Invoke();
    }

    private void CreateTurningPoint()
    {
        var instantiate = Instantiate(AssetPath.PathTurningPoint, _enemySpawner.transform);
        var turningPoint = instantiate.GetComponent<TurningPoint>();
        turningPoint.name = $"Turning Point {Number}-{NumberEnemySpawner}";
        _turningPoints.Add(turningPoint);
    }

    private EnemySpawner DeleteEnemySpawners(Index i)
    {
        var gameObject = _enemySpawners[i];
        gameObject.transform.parent = null;
        return gameObject;
    }

    private TurningPoint DeleteTurningPoints(Index i)
    {
        var gameObject = _turningPoints[i];
        gameObject.transform.parent = null;
        return gameObject;
    }

    private GameObject Instantiate(string path, Transform parent)
    {
        var prefabEnemySpawner = Resources.Load(path);
        var instantiate = (GameObject)Object.Instantiate(prefabEnemySpawner, parent);
        return instantiate;
    }
}