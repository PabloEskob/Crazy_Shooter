using Agava.WebUtility;
using UnityEngine;

namespace Assets.Source.Scripts
{
    public class PauseGameHandler : MonoBehaviour
    {


        private void OnEnable()
        {
            WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
        }

        private void OnDisable()
        {
            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;
        }

        public void OnInBackgroundChange(bool inBackground)
        {
            Time.timeScale = inBackground ? 0.0f : 1.0f;
            //AudioListener.pause = inBackground;
            //AudioListener.volume = inBackground ? 0f : 1f;
        }
    }
}
