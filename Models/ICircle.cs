namespace ASPClicker.Models
{
    public interface ICircle
    {
        double Points { get; set; }
        double ClickPower { get; set; }
        double CritChance { get; set; }
        double Discount { get; set; }
        double ClickMultiplier { get; set; }
        double ClickPowerPercentage { get; set; }
        List<Upgrade> Upgrades { get; set; }

        double CalculateClickPower();
    }
}