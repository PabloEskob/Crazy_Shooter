using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Scripts.UI.Menus.Rewards
{
    public class DailyRewardDisplay : MonoBehaviour
    {
        [SerializeField] private GetButtonTextDisplay _buttonTextDisplay;
        [SerializeField] private Reward _reward;
        [SerializeField] private Reward _rewardReplacement;
        [SerializeField] private Button _getRewardButton;
        [SerializeField] private Text _amountText;
        [SerializeField] private Text _dayText;
        [SerializeField] private Text _dayPhrase;
        [SerializeField] private int _day;

        [Header("Image Dependencies")]
        [SerializeField] private Image _rewardIcon;
        [SerializeField] private Image _background;
        [SerializeField] private Image _rewardIconBackground;
        [SerializeField] private Sprite _recievedRewardIcon;
        [SerializeField] private Sprite _avaliableRewardIcon;
        [SerializeField] private Sprite _nextRewardIcon;
        [SerializeField] private Sprite _currentDayButtonSprite;
        [SerializeField] private Sprite _unavaliableButtonSprite;
        [SerializeField] private Image _coverImage;

        [SerializeField] private bool _isReplaceable;

        public int Day => _day;
        public Button GetRewardButton => _getRewardButton;
        public Reward Reward => _reward;

        private void Start()
        {
            _rewardIcon.sprite = _reward.GetSprite();
            _amountText.text = $"x{_reward.Quantity}";
            _dayText.text = $"{_dayPhrase.text} {_day + 1}";
        }

        public void OnRewardGet()
        {
            _background.sprite = _recievedRewardIcon;
            DeactivateTodayRewardView();
        }

        public void SwitchButtonInteractable(bool value) => 
            _getRewardButton.interactable = value;

        public void DeactivateTodayRewardView()
        {
            SwitchButtonInteractable(false);
            _rewardIconBackground.gameObject.SetActive(false);
            EnableCoverImage();
            _buttonTextDisplay.ChangeText();
        }

        public void ActivateTodayRewardView()
        {
            _background.sprite = _avaliableRewardIcon;
            _coverImage.gameObject.SetActive(false);
            _rewardIconBackground.gameObject.SetActive(true);
            _getRewardButton.image.sprite = _currentDayButtonSprite;
        }

        public void EnableCoverImage() => _coverImage.gameObject.SetActive(true);
        public void DisableRewardIconBackground() => _rewardIconBackground.enabled = false;
    }
}