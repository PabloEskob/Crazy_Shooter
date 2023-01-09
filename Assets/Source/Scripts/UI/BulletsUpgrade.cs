using InfimaGames.LowPolyShooterPack;
using UnityEngine;

namespace Source.Scripts.Ui
{
    public class BulletsUpgrade : UpgradeType
    {
        protected override void OnButtonClick() => 
            SendEvent();

        protected override void OnWeaponSet(Weapon weapon)
        {
            Weapon = weapon;
            SetText();
        }

        public override void SetText() =>
            ButtonText.text = IsUpgradeChoosed ? $"{UpgradeName}-lvl {Weapon.UpgradeConfig.BulletUpgrades[0].Level}" : $"lvl {Weapon.UpgradeConfig.BulletUpgrades[0].Level}";
    }
}