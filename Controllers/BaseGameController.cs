using Microsoft.AspNetCore.Mvc;
using ASPClicker.Models;
using Newtonsoft.Json;

namespace ASPClicker.Controllers
{
    public class BaseGameController : Controller
    {
        private const string GameSessionKey = "GameState";

        private BaseGame GetOrCreateGame()
        {
            if (HttpContext.Session.GetString(GameSessionKey) == null)
            {
                var game = BaseGame.CreateNewGame();
                SaveGame(game);
            }
            return GetGame();
        }

        private BaseGame GetGame()
        {
            var gameJson = HttpContext.Session.GetString(GameSessionKey);
            return JsonConvert.DeserializeObject<BaseGame>(gameJson);
        }

        private void SaveGame(BaseGame game)
        {
            var gameJson = JsonConvert.SerializeObject(game);
            HttpContext.Session.SetString(GameSessionKey, gameJson);
        }

        public IActionResult Index()
        {
            var game = GetOrCreateGame();
            return View(game);
        }

        [HttpPost]
        public IActionResult Click()
        {
            var game = GetGame();
            game.Score += game.ClickMultiplier;
            SaveGame(game);
            return Json(new { score = game.Score, clickMultiplier = game.ClickMultiplier });
        }

        [HttpPost]
        public IActionResult BuyUpgrade(string upgradeId)
        {
            var game = GetGame();
            var upgrade = game.Upgrades.FirstOrDefault(u => u.Id == upgradeId);
            if (upgrade != null && game.Score >= upgrade.Cost)
            {
                game.Score -= upgrade.Cost;
                game.ClickMultiplier += upgrade.Increment;
                upgrade.Count++;
                upgrade.IncreaseCost();
                SaveGame(game);
            }
            return Json(new
            {
                score = game.Score,
                clickMultiplier = game.ClickMultiplier,
                upgradeId = upgrade?.Id,
                cost = upgrade?.Cost,
                count = upgrade?.Count
            });
        }
    }
}