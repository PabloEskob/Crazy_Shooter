public class WaitingEnemyState : IEnemyState
{
    private Enemy _enemy;
    
    public WaitingEnemyState(Enemy enemy)
    {
        _enemy = enemy;
    }

    public void Enter()
    {
        _enemy.EnemyMove.StopMove();
    }

    public void Exit()
    {
    }
}