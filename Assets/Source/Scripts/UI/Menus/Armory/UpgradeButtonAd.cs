using Agava.YandexGames;
using InfimaGames.LowPolyShooterPack;
using Source.Scripts.Ui;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Scripts.UI.Menus.Armory
{
    public class UpgradeButtonAd : MonoBehaviour
    {
        private Weapon _currentWeapon;
        private UpgradeType _currentUpgrade;
        private Button _button;

        public event Action AdShown;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(ShowAd);   
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ShowAd);
        }

        private void ShowAd()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            VideoAd.Show(OnAdStart, null, OnAdClosed);
#endif
#if UNITY_EDITOR
            OnAdClosed();
#endif


        }

        private void OnAdStart()
        {

        }

        private void OnAdClosed()
        {
            AdShown?.Invoke();
        }
    }
}
