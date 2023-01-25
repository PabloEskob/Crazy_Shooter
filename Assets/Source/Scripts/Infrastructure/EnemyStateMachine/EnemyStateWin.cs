using UnityEngine;

public class EnemyStateWin : IEnemyState
{
    private Enemy _enemy;

    public EnemyStateWin(Enemy enemy)
    {
        _enemy = enemy;
    }

    public void Enter()
    {
    }

    public void Exit()
    {
    }
}