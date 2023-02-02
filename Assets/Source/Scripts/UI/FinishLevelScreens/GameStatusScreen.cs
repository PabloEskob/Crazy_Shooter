using UnityEngine;

public class GameStatusScreen : MonoBehaviour
{
    private const string ActorUiTag = "ActorUi";
    
    private SwitchScreen _switchScreen;
    private ActorUI _actorUI;

    public Player Player { get; set; }

    private void OnDisable() =>
        Player.PlayerDeath.OnDied -= PlayerOnDied;

    private void Awake()
    {
        _switchScreen = GetComponentInChildren<SwitchScreen>();
        _actorUI = GameObject.FindGameObjectWithTag(ActorUiTag).GetComponent<ActorUI>();
    }

    private void Start() =>
        Player.PlayerDeath.OnDied += PlayerOnDied;

    public void PlayerVictory()
    {
        _switchScreen.ShowVictoryScreen();
        _actorUI.Disable();
    }

    private void PlayerOnDied()
    {
        _actorUI.Disable();
        _switchScreen.ShowDefeatScreen();
    }
}