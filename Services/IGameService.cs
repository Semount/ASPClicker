using ASPClicker.Models;

namespace ASPClicker.Services
{
    public interface IGameService
    {
        Task<BaseGame> GetOrCreateGame();
        Task SaveGame(BaseGame game);

        bool ClickButton(BaseGame game, string circleType);
        void PurchaseYinUpgrade(BaseGame game, string upgradeId);
        void PurchaseYangUpgrade(BaseGame game, string upgradeId);
        void PurchaseModification(BaseGame game, string modificationId);
    }
}