using ASPClicker.Models;
using ASPClicker.Stores;

public static class UpgradeStore
{
    public static YinUpgradeStore YinStore { get; } = new YinUpgradeStore();
    public static YangUpgradeStore YangStore { get; } = new YangUpgradeStore();
}

public class YinUpgradeStore : IUpgradeStore
{
    private List<Upgrade> upgrades;

    public YinUpgradeStore()
    {
        upgrades = new List<Upgrade>
        {
            new Upgrade { Id = "yin_upgrade1", Cost = 10, Increment = 1, Description = "Купить +1 за клик для Янь" },
            new Upgrade { Id = "yin_upgrade2", Cost = 30, Increment = 2, Description = "Купить +2 за клик для Янь" },
            new Upgrade { Id = "yin_upgrade3", Cost = 100, Increment = 3, Description = "Купить +3 за клик для Янь" }
        };
    }

    public List<Upgrade> GetUpgrades() => upgrades;

    public Upgrade GetUpgrade(string id) => upgrades.FirstOrDefault(u => u.Id == id);

    public void PurchaseUpgrade(ICircle payingCircle, ICircle upgradedCircle, string id, double discount)
    {
        var upgrade = GetUpgrade(id);
        if (upgrade != null && payingCircle.Points >= upgrade.Cost * (1 - discount / 100))
        {
            payingCircle.Points -= upgrade.Cost * (1 - discount / 100);
            upgradedCircle.ClickMultiplier += upgrade.Increment;
            upgradedCircle.ClickPower = upgradedCircle.CalculateClickPower(); // Обновляем ClickPower
            upgrade.Count++;
            upgrade.IncreaseCost();
        }
    }
}

public class YangUpgradeStore : IUpgradeStore
{
    private List<Upgrade> upgrades;

    public YangUpgradeStore()
    {
        upgrades = new List<Upgrade>
        {
            new Upgrade { Id = "yang_upgrade1", Cost = 10, Increment = 0.1, Description = "Купить +0.1 за клик для Инь" },
            new Upgrade { Id = "yang_upgrade2", Cost = 30, Increment = 0.2, Description = "Купить +0.2 за клик для Инь" },
            new Upgrade { Id = "yang_upgrade3", Cost = 100, Increment = 0.3, Description = "Купить +0.3 за клик для Инь" }
        };
    }

    public List<Upgrade> GetUpgrades() => upgrades;

    public Upgrade GetUpgrade(string id) => upgrades.FirstOrDefault(u => u.Id == id);

    public void PurchaseUpgrade(ICircle payingCircle, ICircle upgradedCircle, string id, double discount)
    {
        var upgrade = GetUpgrade(id);
        if (upgrade != null && payingCircle.Points >= upgrade.Cost * (1 - discount / 100))
        {
            payingCircle.Points -= upgrade.Cost * (1 - discount / 100);
            upgradedCircle.ClickMultiplier += upgrade.Increment;
            upgradedCircle.ClickPower = upgradedCircle.CalculateClickPower(); // Обновляем ClickPower
            upgrade.Count++;
            upgrade.IncreaseCost();
        }
    }
}