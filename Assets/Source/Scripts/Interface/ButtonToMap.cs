using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonToMap : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private bool _isSuccess;

    public event Action<bool> Click;

    public void OnPointerClick(PointerEventData eventData) =>
        Click?.Invoke(_isSuccess);
}