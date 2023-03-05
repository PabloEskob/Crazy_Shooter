using UnityEngine;

namespace Assets.Source.Scripts.UI.RouletteWheel
{
    public class RouletteDisplay : MonoBehaviour
    {
        [SerializeField] private Roulette _roulette;

        public Roulette Roulette => _roulette;

        private void OnEnable() => _roulette.Appear();

        private void OnDisable() => _roulette.Disappear();
    }
}
