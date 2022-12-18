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

    public bool IsDied { get; private set; }

    public int Damage { get; set; }

    public float MaxHealth { get; set; }

    public event Action OnDied;
    
    private void OnCollisionEnter(Collision collision)
    {
        TakeDamage(1);
    }

    private void Awake()
    {
        _collider = GetComponent<CapsuleCollider>();
        _enemyAnimator = GetComponent<EnemyAnimator>();
    }

    public void TakeDamage(int damage)
    {
        MaxHealth -= damage;
        
        _particleSystem.Play();

        if (MaxHealth <= 0)
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