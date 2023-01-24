using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMove : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    
    private Enemy _enemy;
    private bool _canMove;
    private Player _player;
    private bool _idleState;
    private Coroutine _coroutine;
    
    public bool CanMove => _canMove;
    public float Speed { get; set; }

    private void Awake() => 
        _enemy = GetComponent<Enemy>();

    private void Start() =>
        _enemy.EnemyAnimator.StopMove();

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
        _enemy.EnemyAnimator.Move();
        _navMeshAgent.speed = Speed;
        _coroutine = StartCoroutine(Move());
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