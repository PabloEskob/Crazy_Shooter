using System;
using UnityEngine;

public class Road : MonoBehaviour
{
    public event Action OnSwitchedOff;
    
    public void TurnOff()
    {
        gameObject.SetActive(false);
        OnSwitchedOff?.Invoke();
    }
    
    public void ReturnToThePlace(Vector3 position)
    {
        transform.localPosition = position;
        gameObject.SetActive(true);
    }
}