using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private MonsterTypeId _monsterTypeId;
    [SerializeField] private int _count;
    [SerializeField] private bool _move = true;
    [SerializeField] private float _spawnDelay;

    [Range(1, 20)] [SerializeField] private int _hp = 5;
    [Range(1, 10)] [SerializeField] private int _damage = 3;
    [Range(1, 20)] [SerializeField] private float _speed = 2;
    [Range(1, 20)] [SerializeField] private float _attackCooldown = 3;
    [Range(1, 20)] [SerializeField] private float _effectiveDistance = 2.5f;

    private IGameFactory _gameFactory;
    private List<Enemy> _enemies;
    private bool _clear;
    private Coroutine _coroutine;

    public bool Clear => _clear;
    public int Number { get; set; }
    public int Hp => _hp;
    public int Damage => _damage;
    public float EffectiveDistance => _effectiveDistance;
    public float Speed => _speed;
    public float AttackCooldown => _attackCooldown;

    public event Action OnClearedSpawner;
    public event Action OnEnemyDied;

    private void OnDisable()
    {
        foreach (var enemy in _enemies)
            enemy.EnemyDeath.OnHappened -= TryTurnOnAnotherSpawner;
    }
    
    public void Construct(IGameFactory gameFactory)
    {
        _gameFactory = gameFactory;
        _enemies = new List<Enemy>();

        CreateQuantityEnemy();

        foreach (var enemy in _enemies)
            enemy.EnemyDeath.OnHappened += TryTurnOnAnotherSpawner;
    }

    public void SetNumber(int number) =>
        Number = number;

    public void TurnOnEnemy() =>
        _coroutine = StartCoroutine(DelayStartTheMoveOfEnemies());

    private Enemy Spawn()
    {
        Enemy enemy =
            _gameFactory.CreateEnemy(_monsterTypeId,
                transform.position + new Vector3(Random.Range(0, 0.1f), 0, Random.Range(0, 0.1f)), _move, this);
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
        enemy.EnemyStateMachine.Enter<WaitingEnemyState>();
        enemy.EnemyMove.Init(_gameFactory.Player);
        enemy.EnemyAttack.Init(_gameFactory);
        enemy.transform.parent = transform;
    }

    private void TryTurnOnAnotherSpawner()
    {
        _count--;
        OnEnemyDied?.Invoke();

        if (_count == 0)
        {
            _clear = true;
            OnClearedSpawner?.Invoke();
        }
    }

    private IEnumerator DelayStartTheMoveOfEnemies()
    {
        var delay = new WaitForSeconds(_spawnDelay);
        yield return delay;

        foreach (var enemy in _enemies)
            enemy.EnemyStateMachine.Enter<MoveEnemyState>();

        StopRoutineDelay();
    }

    private void StopRoutineDelay() =>
        StopCoroutine(_coroutine);
}