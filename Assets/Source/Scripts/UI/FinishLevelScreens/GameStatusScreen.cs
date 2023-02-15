using System;
using PixelCrushers.DialogueSystem;
using Source.Scripts.Music;
using UnityEngine;

public class GameStatusScreen : MonoBehaviour
{
    private SwitchScreen _switchScreen;
    private ActorUI _actorUI;
    private Player _player;
    private SoundZombieScreams _zombieSounds;
    private Coroutine _coroutine;
    private LevelStateMachine _levelStateMachine;
    private string _startMessage = " Месяц после зомби апокалипсиса";

    public bool unregisterOnDisable = false;

    void OnEnable() =>
        Lua.RegisterFunction("DebugLog", this, SymbolExtensions.GetMethodInfo(() => StartState()));

    private void OnDisable()
    {
        _actorUI.OnEnableScreen -= ShowScreen;
        _player.OnSpawnedActorUi -= InitActorUi;

        if (unregisterOnDisable)
            Lua.UnregisterFunction("DebugLog");
    }

    private void Awake()
    {
        _switchScreen = GetComponentInChildren<SwitchScreen>();
        _zombieSounds = GetComponentInChildren<SoundZombieScreams>();
    }

    private void Start()
    {
        if (!string.IsNullOrEmpty(_startMessage)) DialogueManager.ShowAlert(_startMessage);
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

    public void StartState()
    {
        _levelStateMachine.Enter<SpawnEnemyState>();
    }

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