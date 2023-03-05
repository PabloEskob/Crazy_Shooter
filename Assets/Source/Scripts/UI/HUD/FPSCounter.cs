using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _fPSText;
    
    private float _deltaTime;
    
    private bool IsPause => ProjectContext.Instance.PauseService.IsPaused;

    private void Update()
    {
        if (!IsPause)
        {
            _deltaTime += (Time.deltaTime - _deltaTime) * 0.1f;
            float fps = 1.0f / _deltaTime;
            _fPSText.text = Mathf.Ceil(fps)+" fps".ToString();
        }
        
    }
}
