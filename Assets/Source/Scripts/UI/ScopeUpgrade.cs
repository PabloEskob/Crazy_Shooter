using InfimaGames.LowPolyShooterPack;
using UnityEngine;

namespace Source.Scripts.Ui
{
    public class ScopeUpgrade : UpgradeType
    {
        protected override void OnButtonClick() => 
            SendEvent();

        protected override void OnWeaponSet(Weapon weapon)
        {
            Weapon = weapon;
            SetText();
        }

        public override void SetText() =>
            ButtonText.text = IsUpgradeChoosed ? $"{UpgradeName}-lvl {Weapon.UpgradeConfig.ScopeUpgrades[0].Level}" : $"lvl {Weapon.UpgradeConfig.ScopeUpgrades[0].Level}";
    }
}