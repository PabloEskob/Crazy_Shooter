using Assets.Source.Scripts.UI.Menus.Rewards;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Scripts.UI.RouletteWheel
{
    public class WheelSegment : MonoBehaviour
    {
        [SerializeField] private Reward _reward;
        [SerializeField] private Image _image;
        [SerializeField] private Text _quantityText;
        [SerializeField] private bool _isRandom;
        [SerializeField] private int _spinsToGet;

        public int SpinsToGet => _spinsToGet;
        public bool IsRandom => _isRandom;

        public Reward Reward => _reward;

        private void Awake()
        {
            _image.sprite = _reward.GetSprite();
            _quantityText.text = _reward.Quantity.ToString();
        }
    }
}
