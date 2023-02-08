using Source.Scripts.PostProcess;
using UnityEngine;

public class GameStatusScreen : MonoBehaviour
{
    private const string ActorUiTag = "ActorUi";

    private SwitchScreen _switchScreen;
    private ActorUI _actorUI;
    private PostProcess _postProcess;

    private void Awake()
    {
        _switchScreen = GetComponentInChildren<SwitchScreen>();
        _actorUI = GameObject.FindGameObjectWithTag(ActorUiTag).GetComponent<ActorUI>();
        if (Camera.main != null)
            _postProcess = Camera.main.GetComponent<PostProcess>();
    }

    public void PlayVictory()
    {
        _switchScreen.ShowVictoryScreen();
        _actorUI.Disable();
    }

    public void PlayDied()
    {
        _postProcess.enabled = true;
        _switchScreen.ShowDefeatScreen();
        _actorUI.Disable();
    }
}