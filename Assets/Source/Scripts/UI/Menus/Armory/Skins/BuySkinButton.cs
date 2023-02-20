using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Scripts.UI.Menus.Armory.Skins
{
    public class BuySkinButton : MonoBehaviour
    {
        [SerializeField] private Button _buyButton;
        [SerializeField] private Text _priceText;
        [SerializeField] private Text _buttonText;
        [SerializeField] private Text _buyText;
        [SerializeField] private Text _recievedText;

        private string _currentText;

        public Button BuySkinUIButton => _buyButton;

        public void ChangePriceText(string price) => _priceText.text = price;

        public void DisplayButtonText(SkinPlate skinPlate)
        {
            if (skinPlate.IsBought)
                SetText(_recievedText.text);
            else
                SetText(_buyText.text);
        }

        private void SetText(string text)
        {
            _currentText = text;
            _buttonText.text = _currentText;
        }

        public void SwitchButtonInteractable(bool value) => _buyButton.interactable = value;
    }
}
