using System.Collections.Generic;

namespace ASPClicker.Models
{
    public class BaseGame
    {
        public int Score { get; set; }
        public int ClickMultiplier { get; set; }
        public List<Upgrade> Upgrades { get; set; }

        public static BaseGame CreateNewGame()
        {
            return new BaseGame
            {
                Score = 0,
                ClickMultiplier = 1,
                Upgrades = new List<Upgrade>
            {
                new Upgrade { Id = "upgrade1", Cost = 10, Increment = 1, Description = "Купить +1 за клик" },
                new Upgrade { Id = "upgrade2", Cost = 20, Increment = 2, Description = "Купить +2 за клик" },
                new Upgrade { Id = "upgrade3", Cost = 100, Increment = 3, Description = "Купить +3 за клик" },
                new Upgrade { Id = "upgrade4", Cost = 300, Increment = 4, Description = "Купить +4 за клик" },
                new Upgrade { Id = "upgrade5", Cost = 1000, Increment = 5, Description = "Купить +5 за клик" },
                new Upgrade { Id = "upgrade6", Cost = 5000, Increment = 6, Description = "Купить +6 за клик" },
                new Upgrade { Id = "upgrade7", Cost = 12000, Increment = 7, Description = "Купить +7 за клик" },
                new Upgrade { Id = "upgrade8", Cost = 25000, Increment = 8, Description = "Купить +8 за клик" }
            }
            };
        }
    }
}
