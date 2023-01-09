using UnityEngine;

public class GameStatusScreen : MonoBehaviour
{
    public VictoryScreen VictoryScreen { get; private set; }

    private void Awake() =>
        VictoryScreen = GetComponentInChildren<VictoryScreen>();
}