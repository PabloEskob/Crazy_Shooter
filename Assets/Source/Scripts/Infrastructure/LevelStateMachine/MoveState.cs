public class MoveState : ILevelState
{
    private readonly Player _player;
    private LevelStateMachine _levelStateMachine;

    public MoveState(LevelStateMachine levelStateMachine, Player player)
    {
        _levelStateMachine = levelStateMachine;
        _player = player;
    }

    public void Enter()
    {
        _player.Character.NoFire();
        _player.PlayerMove.PlayMove();
        _player.PlayerRotate.RotateReturn();
        _player.PlayerRotate.DisableCameraLock();
        _player.PlayerMove.OnStopped += Stopped;
    }

    public void Exit()
    {
        _player.PlayerMove.OnStopped -= Stopped;
        _player.PlayerMove.StopMove();
        _player.Character.YesFire();
    }

    private void Stopped()
    {
        _levelStateMachine.Enter<SpawnEnemyState>();
    }
}