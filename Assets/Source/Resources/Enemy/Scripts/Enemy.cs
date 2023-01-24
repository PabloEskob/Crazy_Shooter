using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyAttack _enemyAttack;
    private EnemyDeath _enemyDeath;
    private EnemyHealth _enemyHealth;
    private EnemyMove _enemyMove;
    private EnemyAnimator _enemyAnimator;
    private EnemyStateMachine _enemyStateMachine;

    public EnemyStateMachine EnemyStateMachine
    {
        get => _enemyStateMachine ??= new EnemyStateMachine(this);
        set => _enemyStateMachine = value;
    }

    public EnemyHealth EnemyHealth => _enemyHealth;
    public EnemyDeath EnemyDeath => _enemyDeath;
    public EnemyAttack EnemyAttack => _enemyAttack;
    public EnemyMove EnemyMove => _enemyMove;
    public EnemyAnimator EnemyAnimator => _enemyAnimator;

    private void Awake()
    {
        _enemyAttack = GetComponent<EnemyAttack>();
        _enemyAnimator = GetComponent<EnemyAnimator>();
        _enemyDeath = GetComponent<EnemyDeath>();
        _enemyHealth = GetComponent<EnemyHealth>();
        _enemyMove = GetComponent<EnemyMove>();
    }
}