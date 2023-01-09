using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonForward : MonoBehaviour, IPointerClickHandler
{
    public event Action OnClick;
    public event Action Moved;

    public void OnPointerClick(PointerEventData eventData) =>
        OnClick?.Invoke();

    private void Start() =>
        SwitchOff();

    public void SwitchOn()
    {
        gameObject.SetActive(true);
        Moved?.Invoke();
    }

    public void SwitchOff() =>
        gameObject.SetActive(false);
}