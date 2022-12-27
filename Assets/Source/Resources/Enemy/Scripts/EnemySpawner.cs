using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private MonsterTypeId _monsterTypeId;
    [SerializeField] private int _count;
    [SerializeField] private TriggerSpawn _triggerSpawn;

    private int _number;
    private IGameFactory _gameFactory;
    private List<Enemy> _enemies;
    private bool _released;
    private bool _clear;

    public bool Clear => _clear;
    public TriggerSpawn TriggerSpawn => _triggerSpawn;
    public bool Released => _released;
    public int Number => _number;

    public event Action OnTurnedSpawner;

    private void OnDisable()
    {
        foreach (var enemy in _enemies)
            enemy.EnemyDeath.Happened -= TryTurnOnAnotherSpawner;
    }

    private void Start()
    {
        _enemies = new List<Enemy>();

        if (_triggerSpawn != null)
            _triggerSpawn.Init(_number);

        //CreateQuantityEnemy();

        foreach (var enemy in _enemies)
            enemy.EnemyDeath.Happened += TryTurnOnAnotherSpawner;
    }

    public void Init(IGameFactory gameFactory) =>
        _gameFactory = gameFactory;

    public void SetNumber(int number) =>
        _number = number;

    public void TurnOnEnemy()
    {
        foreach (var enemy in _enemies) 
            enemy.gameObject.SetActive(true);

        _released = true;
    }

    private Enemy Spawn()
    {
        Enemy enemy = _gameFactory.CreateEnemy(_monsterTypeId, transform);
        InitEnemy(enemy);
        return enemy;
    }

    public void CreateQuantityEnemy()
    {
        for (int i = 0; i < _count; i++)
            _enemies.Add(Spawn());
    }

    private void InitEnemy(Enemy enemy)
    {
        enemy.GetComponent<EnemyMove>().Init(_gameFactory.Player);
        enemy.GetComponent<EnemyAttack>().Init(_gameFactory);
        enemy.transform.parent = transform;
        enemy.gameObject.SetActive(false);
    }

    private void TryTurnOnAnotherSpawner()
    {
        _count--;

        if (_count != 0) return;
        _clear = true;
        OnTurnedSpawner?.Invoke();
    }
}