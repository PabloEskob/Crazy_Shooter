using PixelCrushers.DialogueSystem;
using Source.Scripts.Music;
using Source.Scripts.Ui;
using UnityEngine;

public class GameStatusScreen : MonoBehaviour
{
    private SwitchScreen _switchScreen;
    private ActorUI _actorUI;
    private Player _player;
    private SoundZombieScreams _zombieSounds;
    private Coroutine _coroutine;
    private LevelStateMachine _levelStateMachine;

    void OnEnable()
    {
        Lua.RegisterFunction("StartEnemySpawn", this, SymbolExtensions.GetMethodInfo(() => StartEnemySpawn()));
        Lua.RegisterFunction("StartFinishState", this, SymbolExtensions.GetMethodInfo(() => StartFinishState()));
    }

    private void OnDisable()
    {
        _actorUI.OnEnableScreen -= ShowScreen;
        _player.OnSpawnedActorUi -= InitActorUi;

        Lua.UnregisterFunction("StartEnemySpawn");
        Lua.UnregisterFunction("StartFinishState");
    }

    private void Awake()
    {
        _switchScreen = GetComponentInChildren<SwitchScreen>();
        _zombieSounds = GetComponentInChildren<SoundZombieScreams>();
    }

    public void PlayVictory()
    {
        _switchScreen.ShowVictoryScreen();
        _actorUI.SwitchOff();
    }

    public void PlayNarrative()
    {
        _switchScreen.CursorVisibility();
        _switchScreen.gameObject.SetActive(false);
    }

    public void StartEnemySpawn() =>
        _levelStateMachine.Enter<SpawnEnemyState>();

    public void StartFinishState() =>
        _levelStateMachine.Enter<FinishState>();

    public void EndNarrative()
    {
        _switchScreen.CursorNoVisibility();
        _switchScreen.gameObject.SetActive(true);
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

    public void StartRoutineSoundZombie() =>
        _coroutine = StartCoroutine(_zombieSounds.PlaySound());

    public void StopRoutineSoundZombie() =>
        StopCoroutine(_coroutine);

    public void SetLevelStateMachine(LevelStateMachine levelStateMachine) =>
        _levelStateMachine = levelStateMachine;

    private void ShowScreen() =>
        _switchScreen.ShowDefeatScreen();

    private void InitActorUi(ActorUI actorUI)
    {
        _actorUI = actorUI;
        _player.PostProcess.enabled = false;
        _actorUI.OnEnableScreen += ShowScreen;
    }
}