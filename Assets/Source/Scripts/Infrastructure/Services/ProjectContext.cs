using System;
using UnityEngine;

public class ProjectContext : MonoBehaviour
{
    public PauseService PauseService { get; private set; }
    public static ProjectContext Instance { get; private set; }

    private void Awake() =>
        Instance = this;

    private void Start() => 
        Initialize();

    public void Initialize() => 
        PauseService = new PauseService();
}