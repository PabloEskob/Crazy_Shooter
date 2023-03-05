using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Scripts.UI.Menus.Rewards
{
    public class DailyRewardMenuButton : MonoBehaviour
    {
        [SerializeField] private DailyRewardsMenu _dailyRewardsMenu;
        [SerializeField] private Button _button;

        private void OnEnable() => _button.onClick.AddListener(OnButtonClicked);

        private void OnDisable() => _button.onClick.RemoveListener(OnButtonClicked);

        private void OnButtonClicked() => _dailyRewardsMenu.Show();
    }
}
