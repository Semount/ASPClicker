using Microsoft.AspNetCore.Mvc;
using ASPClicker.Models;
using ASPClicker.Services;
using ASPClicker.Stores;
using ASPClicker.Extensions;

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
        public async Task<IActionResult> Click(string circleType)
        {
            var game = await _gameService.GetOrCreateGame();
            var isCritical = _gameService.ClickButton(game, circleType);
            ICircle circle = circleType == "Yin" ? (ICircle)game.Yin : (ICircle)game.Yang;

            HttpContext.Session.SetDouble($"{circleType}Points", circle.Points);
            HttpContext.Session.SetDouble($"{circleType}ClickPower", circle.ClickPower);
            HttpContext.Session.SetDouble($"{circleType}CritChance", circle.CritChance);

            return Json(new
            {
                score = circle.Points,
                clickPower = circle.CalculateClickPower(),
                isCritical,
                critChance = circle.CritChance.ToString("F2")
            });
        }

        [HttpPost]
        public async Task<IActionResult> BuyYinUpgrade(string upgradeId)
        {
            var game = await _gameService.GetOrCreateGame();
            _gameService.PurchaseYinUpgrade(game, upgradeId);
            await _gameService.SaveGame(game);

            var upgrade = UpgradeStore.YinStore.GetUpgrade(upgradeId);

            return Json(new
            {
                scoreYin = game.Yin.Points,
                scoreYang = game.Yang.Points,
                clickPowerYin = game.Yin.CalculateClickPower(),
                upgradeId = upgrade?.Id,
                cost = upgrade?.Cost * (1 - game.Yang.Discount / 100),
                originalCost = upgrade?.Cost,
                count = upgrade?.Count,
                discount = game.Yang.Discount
            });
        }

        [HttpPost]
        public async Task<IActionResult> BuyYangUpgrade(string upgradeId)
        {
            var game = await _gameService.GetOrCreateGame();
            _gameService.PurchaseYangUpgrade(game, upgradeId);
            await _gameService.SaveGame(game);

            var upgrade = UpgradeStore.YangStore.GetUpgrade(upgradeId);

            return Json(new
            {
                scoreYin = game.Yin.Points,
                scoreYang = game.Yang.Points,
                clickPowerYang = game.Yang.CalculateClickPower(),
                upgradeId = upgrade?.Id,
                cost = upgrade?.Cost * (1 - game.Yin.Discount / 100),
                originalCost = upgrade?.Cost,
                count = upgrade?.Count,
                discount = game.Yin.Discount
            });
        }

        [HttpPost]
        public async Task<IActionResult> BuyModification(string modificationId)
        {
            if (string.IsNullOrEmpty(modificationId)) return BadRequest("Modification ID is invalid.");

            var game = await _gameService.GetOrCreateGame();
            _gameService.PurchaseModification(game, modificationId);
            await _gameService.SaveGame(game);

            return Json(new
            {
                scoreYin = game.Yin.Points,
                scoreYang = game.Yang.Points,
                clickPowerYin = game.Yin.CalculateClickPower(),
                clickPowerYang = game.Yang.CalculateClickPower(),
                critChanceYin = game.Yin.CritChance.ToString("F2"),
                critChanceYang = game.Yang.CritChance.ToString("F2"),
                discountYin = game.Yin.Discount.ToString("F2"),
                discountYang = game.Yang.Discount.ToString("F2")
            });
        }
    }
}