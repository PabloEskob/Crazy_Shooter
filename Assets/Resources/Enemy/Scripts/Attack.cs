using System.Linq;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimator))]
public class Attack : MonoBehaviour
{
    [SerializeField] private EnemyAnimator _enemyAnimator;
    [SerializeField] private float _attackCooldown = 3;
    [SerializeField] private float _cleavege = 0.5f;

    private readonly Collider[] _hits = new Collider[1];
    private GameFactory _gameFactory;
    private Player _player;
    private float _attackEnd;
    private bool _isAttacking;
    private int _layerMask;
    private bool _attackIsActive;


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
        _layerMask = 1 << LayerMask.NameToLayer("Player");
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
    }

    private void OnAttackEnded()
    {
        _attackEnd = _attackCooldown;
        _isAttacking = false;
        _enemyAnimator.Move();
    }

    private void OnAttack()
    {
        if (Hit(out Collider hit))
            Debug.Log("УДАР");
    }

    private bool Hit(out Collider hit)
    {
        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, _cleavege, _hits, _layerMask);
        hit = _hits.FirstOrDefault();
        return hitCount > 0;
    }

    private bool CooldownIsUp() =>
        _attackEnd <= 0f;

    private bool CanAttack() =>
        _attackIsActive && !_isAttacking && CooldownIsUp();
}