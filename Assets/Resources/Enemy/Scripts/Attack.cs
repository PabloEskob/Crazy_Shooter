using UnityEngine;

[RequireComponent(typeof(EnemyAnimator))]
public class Attack : MonoBehaviour
{
    [SerializeField] private EnemyAnimator _enemyAnimator;

    private float _attackCooldown ;

    public float AttackCooldown
    {
        get => _attackCooldown;
        set => _attackCooldown = value;
    }

    private GameFactory _gameFactory;
    private Player _player;
    private float _attackEnd;
    private bool _isAttacking;
    private bool _attackIsActive;

    public int Damage { get; set; }

    private void Update()
    {
        UpdateCooldown();

        if (CanAttack())
            StartAttack();
    }

    public void Init(GameFactory gameFactory)
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
        transform.LookAt(_player.transform);
        _enemyAnimator.PlayAttack();
        _isAttacking = true;
    }

    private void OnAttackEnded()
    {
        _attackEnd = _attackCooldown;
        _isAttacking = false;
        _enemyAnimator.Move();
    }

    private void OnAttack() =>
        _player.GetComponent<CarHealth>().TakeDamage(Damage);

    private bool CooldownIsUp() =>
        _attackEnd <= 0f;

    private bool CanAttack() =>
        _attackIsActive && !_isAttacking && CooldownIsUp();
}