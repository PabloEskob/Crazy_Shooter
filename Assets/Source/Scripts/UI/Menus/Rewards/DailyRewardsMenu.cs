using UnityEngine;

namespace Assets.Source.Scripts.UI.Menus.Rewards
{
    public class DailyRewardsMenu : MonoBehaviour
    {
        private void Awake()
        {
            Hide();
        }

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}
