using System;
using UnityEngine;


public class ZoneClaws : MonoBehaviour
{
    private RectTransform _rectTransform;

    private void Awake() => 
        _rectTransform = GetComponent<RectTransform>();

    public Vector3 GetBottomLeftCorner()
    {
        Vector3[] vector3S = new Vector3[4];
        _rectTransform.GetWorldCorners(vector3S);
        return vector3S[0];
    }

    public Vector3 SetPosition()
    {
        Vector3 spawnPosition = GetBottomLeftCorner() - new Vector3(UnityEngine.Random.Range(0, _rectTransform.rect.x),
            UnityEngine.Random.Range(0, _rectTransform.rect.y), 0);
        return spawnPosition;
    }
}
