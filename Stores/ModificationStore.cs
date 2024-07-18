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
            if (modification != null && game.Yang.Points >= modification.YangCost && game.Yin.Points >= modification.YinCost)
            {
                game.Yang.Points -= modification.YangCost;
                game.Yin.Points -= modification.YinCost;

                // Apply modification effects to Yin and Yang circles
                ApplyModificationToCircle(modification, game.Yin);
                ApplyModificationToCircle(modification, game.Yang);
            }
        }

        private static void ApplyModificationToCircle(Modification modification, ICircle circle)
        {
            circle.CritChance += modification.CriticalChance;
            circle.Discount += modification.DiscountPercentage;
            circle.ClickPowerPercentage += modification.ClickMultiplierPercentage;
        }

        private static List<Modification> GetDefaultModifications()
        {
            return new List<Modification>
            {
                new Modification
                {
                    Id = "mod1",
                    YangCost = 100,
                    YinCost = 100,
                    Description = "Discount 1% on upgrades",
                    DiscountPercentage = 1,
                    ClickMultiplierPercentage = 0,
                    CriticalChance = 0
                },
                new Modification
                {
                    Id = "mod2",
                    YangCost = 100,
                    YinCost = 100,
                    Description = "Increase click power by 5%",
                    DiscountPercentage = 0,
                    ClickMultiplierPercentage = 5,
                    CriticalChance = 0
                },
                new Modification
                {
                    Id = "mod3",
                    YangCost = 100,
                    YinCost = 100,
                    Description = "Increase critical chance by 1%",
                    DiscountPercentage = 0,
                    ClickMultiplierPercentage = 0,
                    CriticalChance = 1
                }
            };
        }
    }
}