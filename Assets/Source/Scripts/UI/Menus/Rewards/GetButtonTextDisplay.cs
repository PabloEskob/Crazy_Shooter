using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Scripts.UI.Menus.Rewards
{
    public class GetButtonTextDisplay : MonoBehaviour
    {
        [SerializeField] private Text _buttonText;
        [SerializeField] private Text _getText;
        [SerializeField] private Text _recivedText;

        private void Awake() => _buttonText.text = _getText.text;

        public void ChangeText() => _buttonText.text = _recivedText.text;
    }
}
