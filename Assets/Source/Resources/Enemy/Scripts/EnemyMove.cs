using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMove : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;

    private Player _player;
    private Enemy _enemy;
    private float _speed;

    private void Awake() =>
        _enemy = GetComponent<Enemy>();

    private void Update()
    {
        if (!_enemy.EnemyDeath.IsDied)
            _navMeshAgent.destination = _player.transform.position;
        else
        {
            _navMeshAgent.speed = 0;
            _navMeshAgent.enabled = false;
        }
    }

    public void Init(Player player) =>
        _player = player;

    public void StopMove()
    {
        _speed = _navMeshAgent.speed;
        _navMeshAgent.speed = 0;
    }

    public void ContinueMove() =>
        _navMeshAgent.speed = _speed;
}