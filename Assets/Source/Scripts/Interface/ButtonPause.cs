using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPause : MonoBehaviour, IPointerClickHandler
{
    private PanelPause _panelPause;
    private ActorUI _actorUI;
    
    private void Awake() =>
        _actorUI = GetComponentInParent<ActorUI>();

    private void Start() => 
        _panelPause = _actorUI.PanelPause;

    public void OnPointerClick(PointerEventData eventData) => 
        SwitchPanel();

    public void SwitchPanel()
    {
        ProjectContext.Instance.PauseService.SetPaused(!_panelPause.isActiveAndEnabled);
        _panelPause.gameObject.SetActive(!_panelPause.isActiveAndEnabled);
    }
    
}