using ASPClicker.Stores;

namespace ASPClicker.Models
{
    public class BaseGame
    {
        public double Score { get; set; }
        public double ClickPower { get; set; }
        public double ClickMultiplier { get; set; }
        public double ClickPowerPercentage { get; set; }
        public double CritChance {  get; set; }
        public double Discount {  get; set; }
        public List<Upgrade> Upgrades { get; set; }
        public List<Modification> Modifications { get; set; }

        public double CalculateClickPower()
        {
            return (1 + ClickMultiplier) * (1 + ClickPowerPercentage * 0.01);
        }

        public static BaseGame CreateNewGame()
        {
            var game = new BaseGame
            {
                Score = 10000,
                ClickPower = 1,
                ClickMultiplier = 0,
                ClickPowerPercentage = 0,
                CritChance = 0,
                Discount = 0,
                Upgrades = UpgradeStore.GetUpgrades(),
                Modifications = ModificationStore.GetModifications()
            };
            game.ClickPower = game.CalculateClickPower();
            return game;
        }
    }
}
