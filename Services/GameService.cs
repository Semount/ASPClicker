using ASPClicker.Models;
using ASPClicker.Stores;
using System;

namespace ASPClicker.Services
{
    public class GameService : IGameService
    {
        private readonly IGameStateService _gameStateService;
        private readonly Random _random;

        public GameService(IGameStateService gameStateService)
        {
            _gameStateService = gameStateService;
            _random = new Random();
        }

        public async Task<BaseGame> GetOrCreateGame()
        {
            return await _gameStateService.GetOrCreateGame();
        }

        public async Task SaveGame(BaseGame game)
        {
            await _gameStateService.SaveGame(game);
        }

        public async Task<bool> ClickButton(BaseGame game)
        {
            var clickPower = game.CalculateClickPower();
            var isCritical = _random.NextDouble() < game.CritChance / 100;
            game.Score += clickPower * (isCritical ? 2 : 1);
            await SaveGame(game);
            return isCritical;
        }

        public void PurchaseUpgrade(BaseGame game, string upgradeId, double discount)
        {
            UpgradeStore.PurchaseUpgrade(game, upgradeId, discount);
            game.ClickPower = game.CalculateClickPower();
        }

        public void PurchaseModification(BaseGame game, string modificationId)
        {
            ModificationStore.PurchaseModification(game, modificationId);
            game.ClickPower = game.CalculateClickPower();
        }
    }
}