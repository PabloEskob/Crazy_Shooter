using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Player _player;

    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (!_enemy.IsDied)
            _navMeshAgent.destination = _player.transform.position;
        else
            _navMeshAgent.speed = 0;
    }
}