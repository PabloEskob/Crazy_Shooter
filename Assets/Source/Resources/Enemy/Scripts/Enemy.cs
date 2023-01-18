using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyAttack _enemyAttack;
    [SerializeField] private EnemyDeath _enemyDeath;
    [SerializeField] private EnemyHealth _enemyHealth;
    [SerializeField] private EnemyMove _enemyMove;
    [SerializeField] private EnemyAnimator _enemyAnimator;

    public EnemyHealth EnemyHealth => _enemyHealth;
    public EnemyDeath EnemyDeath => _enemyDeath;
    public EnemyAttack EnemyAttack => _enemyAttack;
    public EnemyMove EnemyMove => _enemyMove;
    public EnemyAnimator EnemyAnimator => _enemyAnimator;
}