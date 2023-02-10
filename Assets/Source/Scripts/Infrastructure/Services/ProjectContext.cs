using Agava.WebUtility;
using UnityEngine;

public class ProjectContext : MonoBehaviour
{
    public PauseService PauseService { get; private set; }
    public static ProjectContext Instance { get; private set; }

    private void OnEnable() => 
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;

    private void OnDisable() => 
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;

    private void Awake() =>
        Instance = this;

    private void Start() =>
        Initialize();

    public void OnInBackgroundChange(bool inBackground)
    {
        Time.timeScale = inBackground ? 0.0f : 1.0f;
        PauseService.SetPaused(inBackground);
        AudioListener.pause = inBackground;
        AudioListener.volume = inBackground ? 0f : 1f;
    }

    private void Initialize() =>
        PauseService = new PauseService();
}