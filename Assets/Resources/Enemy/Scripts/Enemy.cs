using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyAttack _enemyAttack;
    [SerializeField] private EnemyDeath _enemyDeath;
    [SerializeField] private EnemyHealth _enemyHealth;

    public EnemyHealth EnemyHealth => _enemyHealth;
    public EnemyDeath EnemyDeath => _enemyDeath;
    public EnemyAttack EnemyAttack => _enemyAttack;

}