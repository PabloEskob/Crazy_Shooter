using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Scripts.UI.Menus.Rewards
{
    public class DailyRewardsMenu : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private List<DailyRewardDisplay> _rewardsDisplay;
        [SerializeField] private DailyRewardHandler _dailyRewardHandler;

        private void OnEnable() => _closeButton.onClick.AddListener(OnCloseButtonClick);

        private void OnDisable() => _closeButton.onClick.RemoveListener(OnCloseButtonClick);

        private void Start()
        {
            _dailyRewardHandler.Load();

            foreach (DailyRewardDisplay display in _rewardsDisplay)
            {
                if (_dailyRewardHandler.GetDay() == display.Day)
                {

                    display.SwitchButtonInteractable(true);
                    display.ActivateTodayRewardView();

                    if (_dailyRewardHandler.CheckDate())
                        display.DeactivateTodayRewardView();
                }
                
                else
                {
                    display.SwitchButtonInteractable(false);
                    display.DisableRewardIconBackground();

                    if (_dailyRewardHandler.GetDay() > display.Day)
                        display.EnableCoverImage();
                }
            }

            Hide();
        }

        private void OnCloseButtonClick() => Hide();

        public void Hide() => gameObject.SetActive(false);

        public void Show() => gameObject.SetActive(true);


    }
}
