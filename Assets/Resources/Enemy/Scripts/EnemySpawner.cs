using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private MonsterTypeId _monsterTypeId;
    [SerializeField] private int _count;
    [SerializeField] private int _number;

    private Player _player;
    private GameFactory _gameFactory;
    private List<Enemy> _enemies;
    private bool _slain;

    public int Number => _number;

    public event Action OnTurnedSpawner;

    private void OnDisable()
    {
        foreach (var enemy in _enemies)
        {
            enemy.OnDied -= TryTurnOnAnotherSpawner;
        }
    }

    private void Awake()
    {
        _enemies = new List<Enemy>();
    }

    private void Start()
    {
        CreateQuantityEnemy();

        foreach (var enemy in _enemies)
        {
            enemy.OnDied += TryTurnOnAnotherSpawner;
        }
    }

    public void TurnOnEnemy()
    {
        foreach (var enemy in _enemies)
        {
            enemy.gameObject.SetActive(true);
        }
    }

    public void SetNumber(int number)
    {
        _number = number;
    }

    public void Init(GameFactory gameFactory, Player player)
    {
        _gameFactory = gameFactory;
        _player = player;
    }

    private Enemy Spawn()
    {
        Enemy enemy = _gameFactory.CreateEnemy(_monsterTypeId, transform);
        TurnOffEnemy(enemy);
        return enemy;
    }

    private void CreateQuantityEnemy()
    {
        for (int i = 0; i < _count; i++)
        {
            _enemies.Add(Spawn());
        }
    }

    private void TurnOffEnemy(Enemy enemy)
    {
        enemy.GetComponent<EnemyMove>().Init(_player);
        enemy.transform.parent = transform;
        enemy.gameObject.SetActive(false);
    }

    private void TryTurnOnAnotherSpawner()
    {
        _count--;

        if (_count != 0) return;
        Debug.Log("Новая волна!");
        OnTurnedSpawner?.Invoke();
    }
}