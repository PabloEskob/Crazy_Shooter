using UnityEngine;

public class GameStatusScreen : MonoBehaviour,IPauseHandler
{
    private SwitchScreen _switchScreen;
    private ActorUI _actorUI;
    private Player _player;

    private void OnDisable()
    {
        _actorUI.OnEnableScreen -= ShowScreen;
        _player.OnSpawnedActorUi -= InitActorUi;
    }

    private void Awake() => 
        _switchScreen = GetComponentInChildren<SwitchScreen>();

    private void Start() => 
        ProjectContext.Instance.PauseService.Register(this);

    public void PlayVictory()
    {
        _switchScreen.ShowVictoryScreen();
        _actorUI.SwitchOff();
    }

    public void PlayDied()
    {
        _player.PostProcess.enabled = true;
        _switchScreen.CursorVisibility();
        _actorUI.EyeClosure.PlayScreen();
    }

    public void InitPlayer(Player player)
    {
        _player = player;
        _player.OnSpawnedActorUi += InitActorUi;
    }

    private void ShowScreen() => 
        _switchScreen.ShowDefeatScreen();

    private void InitActorUi(ActorUI actorUI)
    {
        _actorUI = actorUI;
        _player.PostProcess.enabled = false;
        _actorUI.OnEnableScreen += ShowScreen;
    }

    public void SetPaused(bool isPaused)
    {
        AudioListener.pause = isPaused;
        AudioListener.volume = isPaused ? 0f : 1f;
    }
}