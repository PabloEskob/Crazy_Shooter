using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimator))]
[RequireComponent(typeof(CapsuleCollider))]
public class Enemy : MonoBehaviour, IDamageRecipient
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private float _timeDied = 5f;

    private EnemyAnimator _enemyAnimator;
    private CapsuleCollider _collider;
    private float _maxHealth;
    private int _damage;

    public int Damage
    {
        get => _damage;
        set => _damage = value;
    }

    public float MaxHealth
    {
        get => _maxHealth;
        set => _maxHealth = value;
    }


    public event Action OnDied;

    public bool IsDied { get; private set; }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.GetComponent<Enemy>())
        {
            TakeDamage(1);
        }
    }

    private void Awake()
    {
        _collider = GetComponent<CapsuleCollider>();
        _enemyAnimator = GetComponent<EnemyAnimator>();
    }

    public void TakeDamage(int damage)
    {
        _maxHealth -= damage;
        _particleSystem.Play();

        if (_maxHealth <= 0)
        {
            OnDied?.Invoke();
            Die();
        }
    }

    private void Die()
    {
        IsDied = true;
        _collider.enabled = false;
        _enemyAnimator.PlayDeath();
        StartCoroutine(DisableCharacter());
    }

    private IEnumerator DisableCharacter()
    {
        var newWaitForSecond = new WaitForSeconds(_timeDied);
        yield return newWaitForSecond;
        gameObject.SetActive(false);
    }
}