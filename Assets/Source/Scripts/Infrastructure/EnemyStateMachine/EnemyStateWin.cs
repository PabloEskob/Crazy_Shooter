public class EnemyStateWin : IEnemyState
{
    private readonly Enemy _enemy;

    public EnemyStateWin(Enemy enemy)
    {
        _enemy = enemy;
    }

    public void Enter()
    {
       _enemy.EnemyMove.StartMoveToPlayerDeath();
    }

    public void Exit()
    {
        
    }
}