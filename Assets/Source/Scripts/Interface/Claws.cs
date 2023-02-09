using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Claws : MonoBehaviour
{
    private Image _image;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();
    }
    
    public void Enable()
    {
        _image.fillAmount = 0;
        _image.DOFillAmount(1, 0.3f);
        StartCoroutine(Destroy());
    }

    public void SetPosition(Vector3 vector3) => 
        _rectTransform.position = vector3;

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
