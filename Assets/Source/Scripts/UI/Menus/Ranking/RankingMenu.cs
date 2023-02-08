using System;
using UnityEngine;

namespace Assets.Source.Scripts.UI.Menus.Ranking
{
    public class RankingMenu : MonoBehaviour
    {
        public event Action Activated;

        private void OnEnable()
        {
            Activated?.Invoke();
        }

        private void OnDisable()
        {
            
        }
    }
}
