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
            new Upgrade { Id = "yin_upgrade1", Cost = 10, Increment = 0.5, Description = "Купить +0.5 за клик для Инь" },
            new Upgrade { Id = "yin_upgrade2", Cost = 50, Increment = 1.2, Description = "Купить +1.2 за клик для Инь" },
            new Upgrade { Id = "yin_upgrade3", Cost = 250, Increment = 2.5, Description = "Купить +2.5 за клик для Инь" },
            new Upgrade { Id = "yin_upgrade4", Cost = 1000, Increment = 5.0, Description = "Купить +5.0 за клик для Инь" },
            new Upgrade { Id = "yin_upgrade5", Cost = 5000, Increment = 10.0, Description = "Купить +10.0 за клик для Инь" },
            new Upgrade { Id = "yin_upgrade6", Cost = 25000, Increment = 20.0, Description = "Купить +20.0 за клик для Инь" },
            new Upgrade { Id = "yin_upgrade7", Cost = 100000, Increment = 40.0, Description = "Купить +40.0 за клик для Инь" },
            new Upgrade { Id = "yin_upgrade8", Cost = 500000, Increment = 80.0, Description = "Купить +80.0 за клик для Инь" },
            new Upgrade { Id = "yin_upgrade9", Cost = 2000000, Increment = 160.0, Description = "Купить +160.0 за клик для Инь" }
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
            upgradedCircle.ClickPower = upgradedCircle.CalculateClickPower();
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
            new Upgrade { Id = "yang_upgrade1", Cost = 5, Increment = 0.1, Description = "Купить +0.1 за клик для Янь" },
            new Upgrade { Id = "yang_upgrade2", Cost = 25, Increment = 0.3, Description = "Купить +0.3 за клик для Янь" },
            new Upgrade { Id = "yang_upgrade3", Cost = 100, Increment = 0.7, Description = "Купить +0.7 за клик для Янь" },
            new Upgrade { Id = "yang_upgrade4", Cost = 500, Increment = 1.5, Description = "Купить +1.5 за клик для Янь" },
            new Upgrade { Id = "yang_upgrade5", Cost = 2500, Increment = 3.0, Description = "Купить +3.0 за клик для Янь" },
            new Upgrade { Id = "yang_upgrade6", Cost = 10000, Increment = 6.0, Description = "Купить +6.0 за клик для Янь" },
            new Upgrade { Id = "yang_upgrade7", Cost = 50000, Increment = 12.0, Description = "Купить +12.0 за клик для Янь" },
            new Upgrade { Id = "yang_upgrade8", Cost = 250000, Increment = 25.0, Description = "Купить +25.0 за клик для Янь" },
            new Upgrade { Id = "yang_upgrade9", Cost = 1000000, Increment = 50.0, Description = "Купить +50.0 за клик для Янь" },
            new Upgrade { Id = "yang_upgrade10", Cost = 5000000, Increment = 100.0, Description = "Купить +100.0 за клик для Янь" }
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
            upgradedCircle.ClickPower = upgradedCircle.CalculateClickPower();
            upgrade.Count++;
            upgrade.IncreaseCost();
        }
    }
}