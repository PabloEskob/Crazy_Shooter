public class AttackState : ILevelState
{
    private readonly LevelStateMachine _levelStateMachine;

    private Zone _zone;
    private readonly Player _player;
    private readonly LevelAdjustmentTool _levelAdjustmentTool;
    private GameStatusScreen _gameStatusScreen;

    public AttackState(LevelStateMachine levelStateMachine, Player player, LevelAdjustmentTool levelAdjustmentTool,
        GameStatusScreen gameStatusScreen)
    {
        _player = player;
        _levelStateMachine = levelStateMachine;
        _levelAdjustmentTool = levelAdjustmentTool;
        _gameStatusScreen = gameStatusScreen;
    }

    public void Enter()
    {
        _player.PlayerDeath.OnDied += Death;
        _player.PlayerRotate.EnableCameraLock();
        _zone = _levelAdjustmentTool.GetRoom();
        _gameStatusScreen.StartRoutineSoundZombie();
        _zone.OnRoomCleared += RoomClear;
        _zone.OnNextWave += ClearWave;
    }

    public void Exit()
    {
        _player.PlayerDeath.OnDied -= Death;
        _player.PlayerRotate.CameraLook.StopRoutine();
        _gameStatusScreen.StopRoutineSoundZombie();
        _zone.OnRoomCleared -= RoomClear;
        _zone.OnNextWave -= ClearWave;
    }

    private void RoomClear() =>
        _levelStateMachine.Enter<MoveState>();

    private void ClearWave() =>
        _levelStateMachine.Enter<TurnStateToTarget>();

    private void Death() =>
        _levelStateMachine.Enter<DeathState>();
}