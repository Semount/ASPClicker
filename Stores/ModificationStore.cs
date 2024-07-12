using ASPClicker.Models;

namespace ASPClicker.Stores
{
    public static class ModificationStore
    {
        private static List<Modification> modifications;

        static ModificationStore()
        {
            modifications = GetDefaultModifications();
        }

        public static List<Modification> GetModifications()
        {
            return modifications;
        }

        public static Modification GetModification(string id)
        {
            return modifications.Find(m => m.Id == id);
        }

        public static void PurchaseModification(BaseGame game, string id)
        {
            var modification = GetModification(id);
            if (modification != null && game.Score >= modification.Cost)
            {
                game.Score -= modification.Cost;
                game.ClickPowerPercentage += modification.ClickMultiplierPercentage;
            }
        }

        private static List<Modification> GetDefaultModifications()
        {
            return new List<Modification>
        {
            new Modification
            {
                Id = "mod1",
                Cost = 100,
                Description = "Скидка 10% на улучшение №1",
                DiscountPercentage = 10,
                ClickMultiplierPercentage = 0,
                CriticalChance = 0
            },
            new Modification
            {
                Id = "mod2",
                Cost = 200,
                Description = "Увеличение силы клика на 5%",
                DiscountPercentage = 0,
                ClickMultiplierPercentage = 5,
                CriticalChance = 0
            },
            new Modification
            {
                Id = "mod3",
                Cost = 300,
                Description = "Шанс критического клика 5%",
                DiscountPercentage = 0,
                ClickMultiplierPercentage = 0,
                CriticalChance = 5
            }
        };
        }
    }
}
