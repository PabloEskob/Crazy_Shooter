using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _particleSystem;

    private static readonly int Die = Animator.StringToHash("Die");

    public bool IsDied { get; private set; }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.GetComponent<Enemy>())
        {
            TakeDamage();
        }
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
            _animator.SetTrigger(Die);
        }
    }
}