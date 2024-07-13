using ASPClicker.Models;

namespace ASPClicker.Stores
{
    public static class UpgradeStore
    {
        private static List<Upgrade> upgrades;

        static UpgradeStore()
        {
            upgrades = GetDefaultUpgrades();
        }

        public static List<Upgrade> GetUpgrades()
        {
            return upgrades;
        }

        public static Upgrade GetUpgrade(string id)
        {
            return upgrades.Find(u => u.Id == id);
        }

        public static void PurchaseUpgrade(BaseGame game, string id, double discount)
        {
            var upgrade = GetUpgrade(id);
            if (upgrade != null)
            {
                var discountedCost = upgrade.Cost * (1 - discount / 100);
                if (game.Score >= discountedCost)
                {
                    game.Score -= discountedCost;
                    upgrade.Count++;
                    upgrade.IncreaseCost();
                    game.ClickMultiplier += upgrade.Increment;
                }
            }
        }

        private static List<Upgrade> GetDefaultUpgrades()
        {
            return new List<Upgrade>
            {
                new Upgrade { Id = "upgrade1", Cost = 10, Increment = 1, Description = "Купить +1 за клик" },
                new Upgrade { Id = "upgrade2", Cost = 30, Increment = 2, Description = "Купить +2 за клик" },
                new Upgrade { Id = "upgrade3", Cost = 100, Increment = 3, Description = "Купить +3 за клик" },
                new Upgrade { Id = "upgrade4", Cost = 300, Increment = 4, Description = "Купить +4 за клик" },
                new Upgrade { Id = "upgrade5", Cost = 800, Increment = 5, Description = "Купить +5 за клик" },
                new Upgrade { Id = "upgrade6", Cost = 2000, Increment = 6, Description = "Купить +6 за клик" },
                new Upgrade { Id = "upgrade7", Cost = 5000, Increment = 7, Description = "Купить +7 за клик" },
                new Upgrade { Id = "upgrade8", Cost = 10000, Increment = 8, Description = "Купить +8 за клик" }
        };
        }
    }
}
