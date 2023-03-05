using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMove : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private Enemy _enemy;
    private bool _canMove;
    private Player _player;
    private bool _idleState;
    private Coroutine _coroutine;

    public bool CanMove => _canMove;

    public float Speed { get; set; }

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _enemy = GetComponent<Enemy>();
    }

    private void Start() =>
        transform.LookAt(_player.transform);

    public void SetCanMove(bool move) =>
        _canMove = move;

    public void Init(Player player) =>
        _player = player;

    public void StopMove()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _navMeshAgent.speed = 0;
    }

    public void ContinueMove()
    {
        _canMove = true;
        _navMeshAgent.speed = Speed;
    }

    public void StartMove()
    {
        _enemy.WaypointIndicator.enabled = true;
        _navMeshAgent.speed = Speed;
        _enemy.EnemyAnimator.Move(_navMeshAgent.speed);
        _coroutine = StartCoroutine(Move());
    }

    public void StartMoveToPlayerDeath()
    {
        _navMeshAgent.speed = Speed;
        _navMeshAgent.stoppingDistance = 2f;
        _coroutine = StartCoroutine(MoveDeath());
    }

    private IEnumerator MoveDeath()
    {
        while (_navMeshAgent.stoppingDistance <= _navMeshAgent.remainingDistance)
        {
            _navMeshAgent.destination = _player.transform.position;
            yield return null;
        }
        
        _enemy.EnemyAnimator.Eat();
        StopMove();
    }

    private IEnumerator Move()
    {
        while (true)
        {
            _navMeshAgent.destination = _player.transform.position;
            yield return null;
        }
    }
}