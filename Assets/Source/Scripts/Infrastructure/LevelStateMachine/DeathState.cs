public class DeathState : ILevelState
{
    private readonly Player _player;
    private readonly GameStatusScreen _gameStatusScreen;

    public DeathState(Player player, GameStatusScreen gameStatusScreen)
    {
        _player = player;
        _gameStatusScreen = gameStatusScreen;
    }

    public void Enter()
    {
        _player.PlayerAnimator.PlayDeath();
        _player.SetDisable();
        _gameStatusScreen.PlayDied();
    }

    public void Exit()
    {
    }
}