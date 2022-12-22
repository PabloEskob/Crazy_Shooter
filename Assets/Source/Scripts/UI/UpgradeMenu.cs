using System;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.Ui
{
    public class UpgradeMenu : MonoBehaviour
    {
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _gunFrameUpgradeViewButton;
        [SerializeField] private Button _muzzleUpgradeViewButton;
        [SerializeField] private Button _scopeUpgradeViewButton;
        [SerializeField] private Button _bulletsUpgradeViewButton;
        [SerializeField] private Button _magazineUpgradeViewButton;
        [SerializeField] private Button _buyUpgradeButton;

        private void OnEnable()
        {
            _exitButton.onClick.AddListener(Hide);
        }

        private void OnDisable()
        {
            _exitButton.onClick.RemoveListener(Hide);
        }

        private void Hide() =>
            gameObject.SetActive(false);

        public void Show() =>
            gameObject.SetActive(true);
    }
}
