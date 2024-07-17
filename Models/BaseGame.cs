using ASPClicker.Stores;

namespace ASPClicker.Models
{
    public class BaseGame
    {
        public YinCircle Yin { get; set; }
        public YangCircle Yang { get; set; }
        public double ClickPower { get; set; }
        public double ClickMultiplier { get; set; }
        public double ClickPowerPercentage { get; set; }
        public double CritChance { get; set; }
        public double Discount { get; set; }
        public List<Modification> Modifications { get; set; }

        public static BaseGame CreateNewGame()
        {
            var game = new BaseGame
            {
                Yin = new YinCircle(),
                Yang = new YangCircle(),
                ClickPower = 1,
                ClickMultiplier = 0,
                ClickPowerPercentage = 0,
                CritChance = 0,
                Discount = 0,
                Modifications = ModificationStore.GetModifications()
            };
            return game;
        }
    }
}