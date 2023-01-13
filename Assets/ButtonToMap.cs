using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonToMap : MonoBehaviour, IPointerClickHandler
{
    public event Action Click;

    public void OnPointerClick(PointerEventData eventData) =>
        Click?.Invoke();
}