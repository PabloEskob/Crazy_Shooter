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
        [SerializeField] private Button _buyUpgradeButton;
        [SerializeField] private Button _buySkinButton;
        [SerializeField] private Button _swichMenuButton;

        private void OnEnable()
        {
            _swichMenuButton.onClick.AddListener(Switch);
        }

        private void OnDisable()
        {
            _swichMenuButton.onClick.RemoveListener(Switch);
        }

        private void Switch()
        {
            if (_elementPanel.gameObject.activeInHierarchy)
            {
                Disable(_elementPanel.gameObject);
                Disable(_weaponPlatesView.gameObject);
                Enable(_skinsPanel.gameObject);
                _weaponPlaceMover.MoveTo(_weaponPlaceMover.ZoomedPosition, _weaponPlaceMover.MaxScale);
            }
            else
            {
                Disable(_skinsPanel.gameObject);
                Enable(_elementPanel.gameObject);
                Enable(_weaponPlatesView.gameObject);
                _weaponPlaceMover.MoveTo(_weaponPlaceMover.DefaultPosition, _weaponPlaceMover.MinScale);
            }
        }

        private void Disable(GameObject gameObject)
        {
            gameObject.SetActive(false);
        }

        private void Enable(GameObject gameObject)
        {
            gameObject.SetActive(true);
        }
    }
}
