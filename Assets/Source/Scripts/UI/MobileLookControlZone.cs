using System.Collections;
using System.Collections.Generic;
using InfimaGames.LowPolyShooterPack;
using UnityEngine;

public class MobileLookControlZone : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private CameraLook _cameraLook;
    private void OnMouseDrag()
    {
        float XAxis = Input.GetAxis("Mouse X") * _speed;
        float YAxis = Input.GetAxis("Mouse Y") * _speed;
        
        // _cameraLook.
    }
}
