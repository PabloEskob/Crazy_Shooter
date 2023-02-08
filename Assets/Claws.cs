using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Claws : MonoBehaviour
{
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Start()
    {
        _image.fillAmount = 0;
        _image.DOFillAmount(1, 0.3f);
        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
