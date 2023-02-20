using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Scripts.UI.LevelRewards
{
    public class LevelRewardView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Text _rewardAmountText;

        public void Render(string amount) => _rewardAmountText.text = amount;
    }
}
