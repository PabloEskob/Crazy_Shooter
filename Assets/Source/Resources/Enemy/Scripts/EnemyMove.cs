using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMove : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;

    private Enemy _enemy;
    private float _speed;
    private bool _canMove;
    private Player _player;
    private bool _idleState;
    private Coroutine _coroutine;

    private void Awake() =>
        _enemy = GetComponent<Enemy>();

    private void Start() =>
        _enemy.EnemyAnimator.StopMove();

    /*private void Update()
    {
        if (_canMove && _idleState==false)
        {
            if (!_enemy.EnemyDeath.IsDied)
            {
                _enemy.EnemyAnimator.Move();
                _navMeshAgent.destination = _player.transform.position;
            }
            else
            {
                _navMeshAgent.speed = 0;
                _navMeshAgent.enabled = false;
            }
        }
    }*/

    public void CanMove(bool move) =>
        _canMove = move;

    public void Init(Player player) =>
        _player = player;

    public void StopMove()
    {
        if (_coroutine!=null)
        {
            StopCoroutine(_coroutine);
        }
        _speed = _navMeshAgent.speed;
        _navMeshAgent.speed = 0;
    }

    public void ContinueMove()
    {
        _canMove = true;
        _navMeshAgent.speed = _speed;
    }

    public void StartMove()
    {
        _coroutine = StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (true)
        {
            _enemy.EnemyAnimator.Move();
            _navMeshAgent.destination = _player.transform.position;
            yield return null;
        }
    }
}