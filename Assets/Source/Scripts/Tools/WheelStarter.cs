using Assets.Source.Scripts.UI.RouletteWheel;
using UnityEngine;

namespace Assets.Source.Scripts.Tools
{
    public class WheelStarter : MonoBehaviour
    {
        [SerializeField] private Roulette _roulette;


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
                _roulette.SpinRoulette();
        }
    }
}
