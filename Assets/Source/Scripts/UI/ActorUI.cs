using UnityEngine;

public class ActorUI : MonoBehaviour
{
    private const string PlayerTag = "Player";

    [SerializeField] private ProgressBarPro _hpBar;
    [SerializeField] private CanvasGroup _imageRedScreen;

    private PlayerHealth _playerHealth;
    private GameStatusScreen _gameStatusScreen;
    
    private void Start()
    {
        QualitySettings.SetQualityLevel(0);
        Debug.Log(QualitySettings.GetQualityLevel());
        _playerHealth = GameObject.FindGameObjectWithTag(PlayerTag).GetComponent<PlayerHealth>();
        Construct(_playerHealth);
    }

    private void Construct(PlayerHealth playerHealth)
    {
        _playerHealth = playerHealth;
        _hpBar.SetMaxHpImage(_playerHealth.Max);
        _hpBar.SetValueImage(_imageRedScreen);
        _playerHealth.HealthChanged += UpdateHpBar;
        _playerHealth.Disabled += OnHealthDisabled;
    }
    
    private void OnHealthDisabled()
    {
        _playerHealth.HealthChanged -= UpdateHpBar;
        _playerHealth.Disabled -= OnHealthDisabled;
    }

    
    private void UpdateHpBar() =>
        _hpBar.SetValue(_playerHealth.Current, _playerHealth.Max);
    
}