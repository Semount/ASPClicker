using ASPClicker.Models;
using Newtonsoft.Json;

namespace ASPClicker.Services
{
    public class GameStateService : IGameStateService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string GameSessionKey = "GameState";

        public GameStateService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseGame> GetOrCreateGame()
        {
            if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.Session != null)
            {
                var gameJson = _httpContextAccessor.HttpContext.Session.GetString(GameSessionKey);
                if (gameJson != null)
                {
                    return JsonConvert.DeserializeObject<BaseGame>(gameJson);
                }
            }

            var game = BaseGame.CreateNewGame();
            await SaveGame(game);
            return game;
        }

        public async Task SaveGame(BaseGame game)
        {
            await Task.Run(() =>
            {
                if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.Session != null)
                {
                    var gameJson = JsonConvert.SerializeObject(game);
                    _httpContextAccessor.HttpContext.Session.SetString(GameSessionKey, gameJson);
                }
            });
        }
    }
}
