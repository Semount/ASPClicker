using Microsoft.AspNetCore.Mvc;
using ASPClicker.Models;
using Newtonsoft.Json;
using ASPClicker.Services;
using ASPClicker.Stores;

namespace ASPClicker.Controllers
{
    public class BaseGameController : Controller
    {
        private readonly IGameService _gameService;

        public BaseGameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        public async Task<IActionResult> Index()
        {
            var game = await _gameService.GetOrCreateGame();
            return View(game);
        }

        [HttpPost]
        public async Task<IActionResult> Click()
        {
            var game = await _gameService.GetOrCreateGame();
            await _gameService.ClickButton(game);
            return Json(new { score = game.Score, clickMultiplier = game.ClickMultiplier });
        }

        [HttpPost]
        public async Task<IActionResult> BuyUpgrade(string upgradeId)
        {
            if (string.IsNullOrEmpty(upgradeId)) return BadRequest("Upgrade ID is invalid.");

            var game = await _gameService.GetOrCreateGame();
            _gameService.PurchaseUpgrade(game, upgradeId);
            await _gameService.SaveGame(game);

            var upgrade = UpgradeStore.GetUpgrade(upgradeId);
            return Json(new
            {
                score = game.Score,
                clickMultiplier = game.ClickMultiplier,
                upgradeId = upgrade?.Id,
                cost = upgrade?.Cost,
                count = upgrade?.Count
            });
        }

        [HttpPost]
        public async Task<IActionResult> BuyModification(string modificationId)
        {
            if (string.IsNullOrEmpty(modificationId)) return BadRequest("Modification ID is invalid.");

            var game = await _gameService.GetOrCreateGame();
            _gameService.PurchaseModification(game, modificationId);
            await _gameService.SaveGame(game);

            var modification = ModificationStore.GetModification(modificationId);
            return Json(new
            {
                score = game.Score,
                modificationId = modification?.Id,
                cost = modification?.Cost,
                count = modification?.Count
            });
        }
    }
}
