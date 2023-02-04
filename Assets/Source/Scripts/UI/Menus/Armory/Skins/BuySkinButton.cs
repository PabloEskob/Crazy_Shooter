using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Scripts.UI.Menus.Armory.Skins
{
    public class BuySkinButton : MonoBehaviour
    {
        [SerializeField] private Button _buyButton;
        [SerializeField] private Text _priceText;

        public Button BuySkinUIButton => _buyButton;

        public void ChangePriceText(string price) => _priceText.text = price;

        public void SwitchButtonInteractable(bool value) => _buyButton.interactable = value;
    }
}
