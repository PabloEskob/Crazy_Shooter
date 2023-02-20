using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Scripts.UI.Menus.Armory
{
    public class MenuSwitcher : MonoBehaviour
    {
        [SerializeField] private SkinsPanel _skinsPanel;
        [SerializeField] private ElementPanel _elementPanel;
        [SerializeField] private WeaponPlatesView _weaponPlatesView;
        [SerializeField] private WeaponPlaceMover _weaponPlaceMover;
        [SerializeField] private Button _swichMenuButton;
        [SerializeField] private Button _buyUpgradeButton;
        [SerializeField] private Button _buySkinButton;
        [SerializeField] private Button _adSkinButton;
        [SerializeField] private Button _adUpgradeButton;
        [SerializeField] private Text _switchMenuButtonText;
        [SerializeField] private Text _upgradeText;
        [SerializeField] private Text _skinsText;

        private void OnEnable()
        {
            SetSwitchButtonText(_skinsText.text);
            _swichMenuButton.onClick.AddListener(Switch);
        }

        private void OnDisable() => _swichMenuButton.onClick.RemoveListener(Switch);

        private void Disable(GameObject gameObject) => gameObject.SetActive(false);

        private void Enable(GameObject gameObject) => gameObject.SetActive(true);

        private void Switch()
        {
            if (_elementPanel.gameObject.activeInHierarchy)
            {
                Disable(_elementPanel.gameObject);
                Disable(_weaponPlatesView.gameObject);
                Disable(_adUpgradeButton.gameObject);
                Disable(_buyUpgradeButton.gameObject);
                Enable(_skinsPanel.gameObject);
                SetSwitchButtonText(_upgradeText.text);
                _weaponPlaceMover.MoveTo(_weaponPlaceMover.ZoomedPosition, _weaponPlaceMover.MaxScale);
            }
            else
            {
                Disable(_skinsPanel.gameObject);
                Disable(_adSkinButton.gameObject);
                Disable(_buySkinButton.gameObject);
                Enable(_adUpgradeButton.gameObject);
                Enable(_buyUpgradeButton.gameObject);
                Enable(_elementPanel.gameObject);
                Enable(_weaponPlatesView.gameObject);
                SetSwitchButtonText(_skinsText.text);
                _weaponPlaceMover.MoveTo(_weaponPlaceMover.DefaultPosition, _weaponPlaceMover.MinScale);
            }
        }

        private void SetSwitchButtonText(string text) => _switchMenuButtonText.text = text;
    }
}
