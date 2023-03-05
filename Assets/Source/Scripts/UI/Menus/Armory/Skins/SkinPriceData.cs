using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Scripts.UI.Menus.Armory.Skins
{
    [CreateAssetMenu(fileName = "NewSkinPriceData", menuName = "StaticData/SkinPriceData")]

    public class SkinPriceData :ScriptableObject
    {
        [SerializeField] private List<int> _prices;

        public int PricecCount => _prices.Count;

        public int GetPriceBuyIndex(int index) => _prices[index];
    }
}
