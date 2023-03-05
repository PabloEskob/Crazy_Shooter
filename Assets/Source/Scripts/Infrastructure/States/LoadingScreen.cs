using System.Collections;
using UnityEngine;

namespace Source.Infrastructure
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _screen;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Hide()
        {
            Show();
            StartCoroutine(FadeIn());
        }

        private void Show()
        {
            gameObject.SetActive(true);
            _screen.alpha = 1;
        }

        private IEnumerator FadeIn()
        {
            float step = 0.03f;

            while (_screen.alpha > 0)
            {
                _screen.alpha -= step;
                yield return new WaitForSeconds(step);
            }

            gameObject.SetActive(false);
        }
    }
}