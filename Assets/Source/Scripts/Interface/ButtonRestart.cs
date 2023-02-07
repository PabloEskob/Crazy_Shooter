using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonRestart : MonoBehaviour, IPointerClickHandler
{
    public event Action OnClickRestart;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickRestart?.Invoke();
    }
}