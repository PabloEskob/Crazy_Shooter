using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private MonsterTypeId _monsterTypeId;
    [SerializeField] private int _count;
    [SerializeField] private bool _move = true;

    [Range(1, 20)] [SerializeField] private int _hp = 5;
    [Range(1, 5)] [SerializeField] private int _damage = 3;
    [Range(1, 20)] [SerializeField] private float _speed = 2;
    [Range(1, 20)] [SerializeField] private float _attackCooldown = 3;

    private int _number;
    private IGameFactory _gameFactory;
    private List<Enemy> _enemies;
    private bool _clear;

    public bool Clear => _clear;
    public int Number => _number;
    public int Hp => _hp;
    public int Damage => _damage;
    public float EffectiveDistance => 3.1f;

    public float Speed => _speed;
    public float AttackCooldown => _attackCooldown;

    public event Action OnTurnedSpawner;

    private void OnDisable()
    {
        foreach (var enemy in _enemies)
            enemy.EnemyDeath.Happened -= TryTurnOnAnotherSpawner;
    }

    public void Construct(IGameFactory gameFactory)
    {
        _gameFactory = gameFactory;

        _enemies = new List<Enemy>();

        CreateQuantityEnemy();

        foreach (var enemy in _enemies)
            enemy.EnemyDeath.Happened += TryTurnOnAnotherSpawner;
    }

    public void SetNumber(int number) =>
        _number = number;

    public void TurnOnEnemy()
    {
        foreach (var enemy in _enemies)
            enemy.gameObject.SetActive(true);
    }

    private Enemy Spawn()
    {
        Enemy enemy = _gameFactory.CreateEnemy(_monsterTypeId, transform, _move, this);
        InitEnemy(enemy);
        return enemy;
    }

    private void CreateQuantityEnemy()
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