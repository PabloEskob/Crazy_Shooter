using Assets.Source.Scripts.Character;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Scripts.UI.Menus
{
    public class GrenadeCountView : MonoBehaviour
    {
        [SerializeField] private GrenadesData _grenadesData;
        [SerializeField] private Text _countText;

        private void Awake()
        {
            SetText(_grenadesData.GrenadeCount.ToString());
        }

        private void OnEnable()
        {
            _grenadesData.GrenadeCountChanged += OnGrenadeCountChanged;
        }

        private void OnDisable()
        {
            _grenadesData.GrenadeCountChanged -= OnGrenadeCountChanged;
        }

        private void OnGrenadeCountChanged(int count)
        {
            SetText(count.ToString());
        }

        private void SetText(string text)
        {
            _countText.text = text;
        }
    }
}
