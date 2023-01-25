public class AttackEnemyState : IEnemyState
{
    private readonly Enemy _enemy;
    public AttackEnemyState(Enemy enemy)
    {
        _enemy = enemy;
    }

    public void Enter()
    {
        _enemy.EnemyAttack.StartAttackRoutine();
    }

    public void Exit()
    {
       _enemy.EnemyAttack.StopAttack();
    }
}