using UnityEngine;

public class StateOfDeathEnemy : IEnemyState
{
    private Enemy _enemy;

    public StateOfDeathEnemy(Enemy enemy)
    {
        _enemy = enemy;
    }

    public void Enter()
    {
        _enemy.EnemyAnimator.PlayDeath();
        _enemy.EnemyHealth.Effects.PlayDeath();
    }

    public void Exit()
    {
        Debug.Log("Exit");
    }
}