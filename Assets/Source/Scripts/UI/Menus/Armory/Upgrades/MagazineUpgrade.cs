using InfimaGames.LowPolyShooterPack;

namespace Source.Scripts.Ui
{
    public class MagazineUpgrade : UpgradeType
    {
        protected override void OnButtonClick() => 
            SendEvent();

        protected override void OnWeaponSet(Weapon weapon)
        {
            SetText();
        }

        public override void SetText() =>
            ButtonText.text = 
            IsUpgradeChoosed ? $"{UpgradeName.text}-{LevelText.text} {Weapon.GetMagazineUpgrade().Level}" :
            $"{LevelText.text} {Weapon.GetMagazineUpgrade().Level}";
    }
}