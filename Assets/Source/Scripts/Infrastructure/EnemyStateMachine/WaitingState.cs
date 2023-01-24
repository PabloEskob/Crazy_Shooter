public class WaitingEnemyState : IEnemyState
{
    private readonly Enemy _enemy;
    private EnemyStateMachine _enemyStateMachine;
    
    public WaitingEnemyState(Enemy enemy,EnemyStateMachine enemyStateMachine)
    {
        _enemy = enemy;
        _enemyStateMachine = enemyStateMachine;
    }

    public void Enter()
    {
        _enemy.EnemyMove.StopMove();
    }

    public void Exit()
    {
        
    }
}