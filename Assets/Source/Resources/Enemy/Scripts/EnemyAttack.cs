using UnityEngine;

[RequireComponent(typeof(EnemyAnimator))]
public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private EnemyAnimator _enemyAnimator;

    private IGameFactory _gameFactory;
    private Player _player;
    private float _attackEnd;
    private bool _isAttacking;
    private bool _attackIsActive;
    private PlayerHealth _playerHealth;
    private PlayerDeath _playerDeath;
    private bool _stopAttack;

    public float AttackCooldown { get; set; }
    public int Damage { get; set; }

    private void Update()
    {
        if (_stopAttack) return;
        UpdateCooldown();

        if (CanAttack())
            StartAttack();
    }

    private void Start()
    {
        _playerHealth = _player.GetComponent<PlayerHealth>();
        _playerDeath = _player.GetComponent<PlayerDeath>();
    }

    public void Init(IGameFactory gameFactory)
    {
        _gameFactory = gameFactory;
        _player = _gameFactory.Player;
    }

    public void EnableAttack() =>
        _attackIsActive = true;

    public void DisableAttack() =>
        _attackIsActive = false;

    private void UpdateCooldown()
    {
        if (!CooldownIsUp())
            _attackEnd -= Time.deltaTime;
    }

    private void StartAttack()
    {
        if (!_playerDeath.IsDead)
        {
            transform.LookAt(_player.transform);
            _enemyAnimator.PlayAttack();
            _isAttacking = true;
        }
        else
            _stopAttack = true;
    }

    private void OnAttackEnded()
    {
        _enemyAnimator.PlayIdle();
        _attackEnd = AttackCooldown;
        _isAttacking = false;
    }

    private void OnAttack()
    {
        _playerHealth.TakeDamage(Damage);
    }

    private bool CooldownIsUp() =>
        _attackEnd <= 0f;

    private bool CanAttack() =>
        _attackIsActive && !_isAttacking && CooldownIsUp();
}