public class FinishState : ILevelState
{
    private readonly Player _player;
    private readonly FinishLevel _finishLevel;
    private readonly GameStatusScreen _gameStatusScreen;

    public FinishState( Player player, FinishLevel finishLevel,GameStatusScreen gameStatusScreen)
    {
        _gameStatusScreen = gameStatusScreen;
        _finishLevel = finishLevel;
        _player = player;
    }

    public void Enter()
    {
        _finishLevel.OpenChest();
        _player.PlayerRotate.CameraLook.StartRotateToFinish(_finishLevel.TurningPoint);
        _gameStatusScreen.PlayVictory();
    }

    public void Exit()
    {
       
    }
}