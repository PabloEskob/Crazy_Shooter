using InfimaGames.LowPolyShooterPack;
using UnityEngine;

namespace Source.Scripts.Ui
{
    public class FrameUpgrade : UpgradeType
    {
        protected override void OnButtonClick() => 
            SendEvent();

        protected override void OnWeaponSet(Weapon weapon)
        {
            SetText();
        }

        public override void SetText()
        {
            Debug.Log($"{UpgradeName.text}-{LevelText.text} {Weapon.GetFrameUpgrade().Level}");
            ButtonText.text =
            IsUpgradeChoosed ? $"{UpgradeName.text}-{LevelText.text} {Weapon.GetFrameUpgrade().Level}" :
            $"{LevelText.text} {Weapon.GetFrameUpgrade().Level}";
        }
    }
}