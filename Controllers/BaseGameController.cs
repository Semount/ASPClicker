using Microsoft.AspNetCore.Mvc;
using ASPClicker.Models;
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
            var isCritical = await _gameService.ClickButton(game);
            return Json(new
            {
                score = game.Score,
                clickPower = game.CalculateClickPower(),
                isCritical,
                critChance = game.CritChance
            });
        }

        [HttpPost]
        public async Task<IActionResult> BuyUpgrade(string upgradeId)
        {
            if (string.IsNullOrEmpty(upgradeId)) return BadRequest("Upgrade ID is invalid.");

            var game = await _gameService.GetOrCreateGame();
            var upgrade = UpgradeStore.GetUpgrade(upgradeId);

            var discountedCost = upgrade.Cost * (1 - game.Discount / 100);

            if (game.Score >= discountedCost)
            {
                _gameService.PurchaseUpgrade(game, upgradeId, game.Discount);
                await _gameService.SaveGame(game);
                return Json(new
                {
                    score = game.Score,
                    clickMultiplier = game.ClickMultiplier,
                    upgradeId = upgrade?.Id,
                    cost = upgrade?.Cost * (1 - game.Discount / 100),
                    count = upgrade?.Count,
                    discount = game.Discount,
                    clickPower = game.CalculateClickPower(),
                    critChance = game.CritChance
                });
            }
            else
            {
                return Json(new { error = "Insufficient funds" });
            }
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
                clickPower = game.CalculateClickPower(),
                clickMultiplier = game.ClickMultiplier,
                clickPowerPercentage = game.ClickPowerPercentage,
                discount = game.Discount,
                critChance = game.CritChance,
                modificationId = modification?.Id,
                cost = modification?.Cost
            });
        }
    }
}