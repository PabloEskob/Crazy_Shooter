using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Scripts.UI.LevelRewards
{
    public class ExtraRewardView : MonoBehaviour
    {
        [SerializeField] private Image _icon;

        public void Render(Sprite icon) => _icon.sprite = icon;
    }
}
