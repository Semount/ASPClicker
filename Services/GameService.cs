using ASPClicker.Models;
using ASPClicker.Stores;

namespace ASPClicker.Services
{
    public class GameService : IGameService
    {
        private readonly IGameStateService _gameStateService;

        public GameService(IGameStateService gameStateService)
        {
            _gameStateService = gameStateService;
        }

        public async Task<BaseGame> GetOrCreateGame()
        {
            return await _gameStateService.GetOrCreateGame();
        }

        public async Task SaveGame(BaseGame game)
        {
            await _gameStateService.SaveGame(game);
        }

        public async Task ClickButton(BaseGame game)
        {
            game.Score += game.ClickPower;
            await SaveGame(game);
        }

        public void PurchaseUpgrade(BaseGame game, string upgradeId)
        {
            UpgradeStore.PurchaseUpgrade(game, upgradeId);
            game.ClickPower = game.CalculateClickPower();
        }

        public void PurchaseModification(BaseGame game, string modificationId)
        {
            ModificationStore.PurchaseModification(game, modificationId);
            game.ClickPower = game.CalculateClickPower();
        }
    }
}
