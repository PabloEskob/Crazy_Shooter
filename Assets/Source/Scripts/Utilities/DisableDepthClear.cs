using UnityEngine;

public class DisableDepthClear : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        _camera = GetComponent<Camera>();
        Debug.Log("ssss");

        if (_camera.clearFlags == CameraClearFlags.Depth)
            _camera.clearFlags = CameraClearFlags.Nothing;
#endif
    }
}
