using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Scripts.UI.Menus.Ranking
{
    public class RankingMenu : MonoBehaviour
    {
        [SerializeField] private Button _exitButton;

        public event Action Activated;

        private void OnEnable()
        {
            _exitButton.onClick.AddListener(OnExitButtonClick);
            Activated?.Invoke();
        }

        private void OnDisable() => _exitButton.onClick.RemoveListener(OnExitButtonClick);

        private void OnExitButtonClick() => Hide();

        private void Hide() => gameObject.SetActive(false);
    }
}
