﻿public class MoveEnemyState : IEnemyState
{
    private readonly Enemy _enemy;
    private readonly EnemyStateMachine _enemyStateMachine;

    public MoveEnemyState(Enemy enemy, EnemyStateMachine enemyStateMachine)
    {
        _enemyStateMachine = enemyStateMachine;
        _enemy = enemy;
    }

    public void Enter()
    {
        _enemy.EnemyAnimator.ChangeAnimation();
        
        _enemy.EnemyHealth.CanHit = true;
        
        switch (_enemy.EnemyMove.CanMove)
        {
            case true:
                _enemy.EnemyMove.StartMove();
                break;
            default:
                _enemyStateMachine.Enter<WaitingEnemyState>();
                break;
        }
    }

    public void Exit()
    {
        _enemy.EnemyMove.StopMove();
    }
}