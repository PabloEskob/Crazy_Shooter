using UnityEngine;

public class GameStatusScreen : MonoBehaviour
{
    private void OnDisable() =>
        _playerDeath.OnDied -= PlayerOnDied;

    private PlayerDeath _playerDeath;
    private SwitchScreen _switchScreen;

    public Player Player { get; set; }

    private void Awake() =>
        _switchScreen = GetComponentInChildren<SwitchScreen>();

    private void Start()
    {
        _playerDeath = Player.GetComponent<PlayerDeath>();
        _playerDeath.OnDied += PlayerOnDied;
    }

    public void PlayerVictory() =>
        _switchScreen.ShowVictoryScreen();

    private void PlayerOnDied() =>
        _switchScreen.ShowDefeatScreen();
}