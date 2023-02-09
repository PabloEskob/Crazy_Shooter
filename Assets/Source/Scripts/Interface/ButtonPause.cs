using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPause : MonoBehaviour, IPointerClickHandler, IPauseHandler
{
    private PanelPause _panelPause;
    private ActorUI _actorUI;
    
    private void Awake() =>
        _actorUI = GetComponentInParent<ActorUI>();

    private void Start()
    {
        ProjectContext.Instance.PauseService.Register(this);
        _panelPause = _actorUI.PanelPause;
    }

    public void OnPointerClick(PointerEventData eventData) => 
        SwitchPanel();

    public void SwitchPanel()
    {
        ProjectContext.Instance.PauseService.SetPaused(!_panelPause.isActiveAndEnabled);
        _panelPause.gameObject.SetActive(!_panelPause.isActiveAndEnabled);
    }

    public void SetPaused(bool isPaused) => 
        Time.timeScale = isPaused ? 0f : 1f;
}