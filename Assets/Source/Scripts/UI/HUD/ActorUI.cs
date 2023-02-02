using UnityEngine;

public class ActorUI : MonoBehaviour
{
    private const string PlayerTag = "Player";

    [SerializeField] private ProgressBarPro _hpBar;
    [SerializeField] private CanvasGroup _imageRedScreen;

    private Player _player;
    private GameStatusScreen _gameStatusScreen;
    
    private void Start()
    {
        QualitySettings.SetQualityLevel(0);
        Debug.Log(QualitySettings.GetQualityLevel());
        _player = GameObject.FindGameObjectWithTag(PlayerTag).GetComponent<Player>();
        Construct();
    }

    public void Disable() => 
        gameObject.SetActive(false);

    private void Construct()
    {
        _hpBar.SetMaxHpImage(_player.PlayerHealth.Max);
        _hpBar.SetValueImage(_imageRedScreen);
        _player.PlayerHealth.HealthChanged += UpdateHpBar;
        _player.PlayerHealth.Disabled += OnHealthDisabled;
    }

    private void OnHealthDisabled()
    {
        _player.PlayerHealth.HealthChanged -= UpdateHpBar;
        _player.PlayerHealth.Disabled -= OnHealthDisabled;
    }

    
    private void UpdateHpBar() =>
        _hpBar.SetValue(_player.PlayerHealth.Current, _player.PlayerHealth.Max);
    
}