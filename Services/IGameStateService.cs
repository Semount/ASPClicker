using ASPClicker.Models;

namespace ASPClicker.Services
{
    public interface IGameStateService
    {
        Task<BaseGame> GetOrCreateGame();
        Task SaveGame(BaseGame game);
        ///Task SaveFullGame(BaseGame game);
        ///Task SavePartialGame(BaseGame game);
    }
}
