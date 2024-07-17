namespace ASPClicker.Models
{
    public class YinCircle : ICircle
    {
        public double Points { get; set; } = 0;
        public double ClickPower { get; set; } = 1;
        public double ClickMultiplier { get; set; } = 0;
        public double ClickPowerPercentage { get; set; } = 0;
        public double CritChance { get; set; } = 0;
        public double Discount { get; set; } = 0;
        public List<Upgrade> Upgrades { get; set; } = new List<Upgrade>();

        public double CalculateClickPower()
        {
            return (100 + ClickMultiplier) * (1 + ClickPowerPercentage * 0.01);
        }

        public YinCircle()
        {
            Upgrades = UpgradeStore.YinStore.GetUpgrades();
        }
    }

    public class YangCircle : ICircle
    {
        public double Points { get; set; } = 0;
        public double ClickPower { get; set; } = 1;
        public double ClickMultiplier { get; set; } = 0;
        public double ClickPowerPercentage { get; set; } = 0;
        public double CritChance { get; set; } = 0;
        public double Discount { get; set; } = 0;
        public List<Upgrade> Upgrades { get; set; } = new List<Upgrade>();

        public double CalculateClickPower()
        {
            return (100 + ClickMultiplier) * (1 + ClickPowerPercentage * 0.01);
        }

        public YangCircle()
        {
            Upgrades = UpgradeStore.YangStore.GetUpgrades();
        }
    }
}