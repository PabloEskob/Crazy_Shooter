using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Attack _attack;
    [SerializeField] private EnemyDeath _enemyDeath;
    [SerializeField] private EnemyHealth _enemyHealth;

    public EnemyHealth EnemyHealth => _enemyHealth;
    public EnemyDeath EnemyDeath => _enemyDeath;
    public Attack Attack => _attack;

}