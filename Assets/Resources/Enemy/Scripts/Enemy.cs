using System;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _particleSystem;

    private static readonly int Die = Animator.StringToHash("Die");
    private CapsuleCollider _collider;

    public bool IsDied { get; private set; }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.GetComponent<Enemy>())
        {
            TakeDamage();
        }
    }

    private void Awake()
    {
        _collider = GetComponent<CapsuleCollider>();
    }

    public void Died()
    {
        gameObject.SetActive(false);
    }

    private void TakeDamage()
    {
        _maxHealth -= 1;
        _particleSystem.Play();
        
        if (_maxHealth == 0)
        {
            IsDied = true;
            _collider.enabled = false;
            _animator.SetTrigger(Die);
        }
    }
}