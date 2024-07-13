using ASPClicker.Models;

namespace ASPClicker.Services
{
    public interface IGameService
    {
        Task<BaseGame> GetOrCreateGame();
        Task SaveGame(BaseGame game);
        Task<bool> ClickButton(BaseGame game);
        void PurchaseUpgrade(BaseGame game, string upgradeId, double discount);
        void PurchaseModification(BaseGame game, string modificationId);
    }
}