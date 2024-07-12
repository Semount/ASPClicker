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
                new Upgrade { Id = "upgrade1", Cost = 10, Increment = 1, Description = "Купить +1 за клик (10 очков)" },
                new Upgrade { Id = "upgrade2", Cost = 20, Increment = 2, Description = "Купить +2 за клик (20 очков)" }
            }
            };
        }
    }
}
