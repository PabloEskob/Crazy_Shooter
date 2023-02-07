using UnityEngine;

public class GameStatusScreen : MonoBehaviour
{
    private const string ActorUiTag = "ActorUi";
    
    private SwitchScreen _switchScreen;
    private ActorUI _actorUI;
    
    private void Awake()
    {
        _switchScreen = GetComponentInChildren<SwitchScreen>();
        _actorUI = GameObject.FindGameObjectWithTag(ActorUiTag).GetComponent<ActorUI>();
    }
    
    public void PlayVictory()
    {
        _switchScreen.ShowVictoryScreen();
        _actorUI.Disable();
    }

    public void PlayDied()
    {
        _switchScreen.ShowDefeatScreen();
        _actorUI.Disable();
    }
}