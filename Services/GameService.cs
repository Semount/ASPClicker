using ASPClicker.Models;
using ASPClicker.Stores;

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

        public bool ClickButton(BaseGame game, string circleType)
        {
            ICircle circle = circleType == "Yin" ? game.Yin : game.Yang;
            var isCritical = _random.NextDouble() < circle.CritChance / 100;
            circle.Points += circle.CalculateClickPower() * (isCritical ? 2 : 1);

            return isCritical;
        }

        public void PurchaseYinUpgrade(BaseGame game, string upgradeId)
        {
            UpgradeStore.YinStore.PurchaseUpgrade(game.Yang, game.Yin, upgradeId, game.Yang.Discount);
            //game.Yin.ClickPower = game.Yin.CalculateClickPower(); // Update ClickPower
        }

        public void PurchaseYangUpgrade(BaseGame game, string upgradeId)
        {
            UpgradeStore.YangStore.PurchaseUpgrade(game.Yin, game.Yang, upgradeId, game.Yin.Discount);
            //game.Yang.ClickPower = game.Yang.CalculateClickPower(); // Update ClickPower
        }

        public void PurchaseModification(BaseGame game, string modificationId)
        {
            ModificationStore.PurchaseModification(game, modificationId);
        }
    }
}