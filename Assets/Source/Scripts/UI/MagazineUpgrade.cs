using InfimaGames.LowPolyShooterPack;

namespace Source.Scripts.Ui
{
    public class MagazineUpgrade : UpgradeType
    {
        protected override void OnButtonClick()
        {
            SendEvent();
            SetText(Weapon.Stats.Level.ToString());
        }

        protected override void OnWeaponSet(Weapon weapon)
        {
            Weapon = weapon;
            SetText(Weapon.Stats.Level.ToString());
        }

        protected override void SetText(string text) => 
            ButtonText.text = IsUpgradeChoosed ? $"{UpgradeName}-{text}" : $"lvl {text}";
    }
}