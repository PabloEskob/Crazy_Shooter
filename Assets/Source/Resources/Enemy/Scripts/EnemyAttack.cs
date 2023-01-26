using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimator))]
public class EnemyAttack : MonoBehaviour
{
    private Enemy _enemy;
    private IGameFactory _gameFactory;
    private Player _player;
    private float _attackEnd;
    private bool _isAttacking;
    private bool _attackIsActive;
    private bool _stopAttack;
    private Coroutine _coroutine;

    public float AttackCooldown { get; set; }
    public int Damage { get; set; }

    private void Awake() => 
        _enemy = GetComponent<Enemy>();

    public void Init(IGameFactory gameFactory)
    {
        _gameFactory = gameFactory;
        _player = _gameFactory.Player;
    }

    public void EnableAttack()
    {
        if (!_enemy.EnemyDeath.IsDie)
        {
            _enemy.EnemyStateMachine.Enter<AttackEnemyState>();
            _attackIsActive = true;
        }
      
    }

    public void DisableAttack() =>
        _attackIsActive = false;

    public void StopAttack() =>
        StopCoroutine(_coroutine);

    public void StartAttackRoutine() =>
        _coroutine = StartCoroutine(Attack());

    private void UpdateCooldown()
    {
        if (!CooldownIsUp())
            _attackEnd -= Time.deltaTime;
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            if (_player.PlayerDeath.IsDead) 
                _enemy.EnemyStateMachine.Enter<EnemyStateWin>();
            
            UpdateCooldown();

            if (CanAttack()) 
                StartAttack();

            yield return null;
        }
    }

    private void StartAttack()
    {
        transform.LookAt(_player.transform);
        _enemy.EnemyAnimator.PlayAttack();
        _isAttacking = true;
    }

    private void OnAttackEnded()
    {
        _enemy.EnemyAnimator.PlayIdle();
        _attackEnd = AttackCooldown;
        _isAttacking = false;
    }

    private void OnAttack()
    {
        _player.PlayerHealth.TakeDamage(Damage);
    }

    private bool CooldownIsUp() =>
        _attackEnd <= 0f;

    private bool CanAttack() =>
        _attackIsActive && !_isAttacking && CooldownIsUp();
}