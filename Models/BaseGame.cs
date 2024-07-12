using ASPClicker.Stores;

namespace ASPClicker.Models
{
    public class BaseGame
    {
        public int Score { get; set; }
        public int ClickMultiplier { get; set; }
        public List<Upgrade> Upgrades { get; set; }
        public List<Modification> Modifications { get; set; }

        public static BaseGame CreateNewGame()
        {
            return new BaseGame
            {
                Score = 0,
                ClickMultiplier = 1,
                Upgrades = UpgradeStore.GetUpgrades(),
                Modifications = ModificationStore.GetModifications()
            };
        }
    }
}
