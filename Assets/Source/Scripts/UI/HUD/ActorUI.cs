using System;
using UnityEngine;

public class ActorUI : MonoBehaviour
{
    private const string PlayerTag = "Player";

    [SerializeField] private ProgressBarPro _hpBar;
    [SerializeField] private TakeDamageImage _imageRedScreen;

    private ClawsSpawner _clawsSpawner;
    private Player _player;
    private EyeClosure _eyeClosure;

    public EyeClosure EyeClosure => _eyeClosure;

    public event Action OnEnableScreen;

    private void OnEnable() =>
        _eyeClosure.OnEndedAnimation += EnableScreen;

    private void OnDisable() =>
        _eyeClosure.OnEndedAnimation -= EnableScreen;

    private void Awake()
    {
        _clawsSpawner = GetComponentInChildren<ClawsSpawner>();
        _eyeClosure = GetComponentInChildren<EyeClosure>();
    }

    private void Start()
    {
        QualitySettings.SetQualityLevel(0);
        _player = GameObject.FindGameObjectWithTag(PlayerTag).GetComponent<Player>();
        Construct();
    }

    public void SwitchOff() =>
        gameObject.SetActive(false);

    private void EnableScreen() =>
        OnEnableScreen?.Invoke();

    private void Construct()
    {
        _hpBar.SetMaxHpImage(_player.PlayerHealth.Max);
        _hpBar.SetValueImage(_imageRedScreen.GetComponent<CanvasGroup>());
        _player.PlayerHealth.HealthChanged += UpdateHpBar;
        _player.PlayerHealth.Disabled += OnHealthDisabled;
    }

    private void OnHealthDisabled()
    {
        _player.PlayerHealth.HealthChanged -= UpdateHpBar;
        _player.PlayerHealth.Disabled -= OnHealthDisabled;
    }

    private void UpdateHpBar()
    {
        _clawsSpawner.Attack();
        _imageRedScreen.ChangeAlpha();
        _hpBar.SetValue(_player.PlayerHealth.Current, _player.PlayerHealth.Max);
    }
}