using ASPClicker.Models;

namespace ASPClicker.Services
{
    public interface IGameService
    {
        Task<BaseGame> GetOrCreateGame();
        Task SaveGame(BaseGame game);
        Task ClickButton(BaseGame game);
        void PurchaseUpgrade(BaseGame game, string upgradeId);
        void PurchaseModification(BaseGame game, string modificationId);
    }
}
