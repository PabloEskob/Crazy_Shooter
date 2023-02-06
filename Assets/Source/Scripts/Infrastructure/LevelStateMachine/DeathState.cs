public class DeathState : ILevelState
{
    private readonly Player _player;

    public DeathState(Player player)
    {
        _player = player;
    }

    public void Enter()
    {
        _player.PlayerAnimator.PlayDeath();
        _player.SetActive();
    }

    public void Exit()
    {
    }
}