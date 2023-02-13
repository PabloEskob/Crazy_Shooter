using Agava.WebUtility;
using UnityEngine;

public class ProjectContext : MonoBehaviour,IPauseHandler
{
    public PauseService PauseService { get; private set; }
    public static ProjectContext Instance { get; private set; }

    private bool _isPlayAds;

    private void OnEnable() => 
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;

    private void OnDisable() => 
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;

    private void Awake() =>
        Instance = this;

    private void Start()
    {
        Initialize();
        PauseService.Register(this);
    }

    public void SetPaused(bool isPaused)
    {
        if (_isPlayAds==false)
        {
            Time.timeScale = isPaused ? 0.0f : 1.0f;
            AudioListener.pause = isPaused;
            AudioListener.volume = isPaused ? 0f : 1f;
        }
    }

    public void SetPauseWhenAds(bool startedPlayingAds, bool isPaused)
    {
        _isPlayAds = startedPlayingAds;
        SetPaused(isPaused);
    }

    private void OnInBackgroundChange(bool inBackground) => 
        PauseService.SetPaused(inBackground);

    private void Initialize() =>
        PauseService = new PauseService();
}