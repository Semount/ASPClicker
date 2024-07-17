using ASPClicker.Extensions;
using ASPClicker.Models;
using Newtonsoft.Json;
using System.Collections.Concurrent;

namespace ASPClicker.Services
{
    public class GameStateService : IGameStateService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string GameSessionKey = "GameState";
        private static readonly object lockObj = new object();

        public GameStateService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseGame> GetOrCreateGame()
        {
            if (_httpContextAccessor.HttpContext?.Session == null)
                return BaseGame.CreateNewGame();

            var game = BaseGame.CreateNewGame();

            var yinPoints = _httpContextAccessor.HttpContext.Session.GetDouble("YinPoints");
            var yangPoints = _httpContextAccessor.HttpContext.Session.GetDouble("YangPoints");
            var yinClickPower = _httpContextAccessor.HttpContext.Session.GetDouble("YinClickPower");
            var yangClickPower = _httpContextAccessor.HttpContext.Session.GetDouble("YangClickPower");
            var yinCritChance = _httpContextAccessor.HttpContext.Session.GetDouble("YinCritChance");
            var yangCritChance = _httpContextAccessor.HttpContext.Session.GetDouble("YangCritChance");
            var yinClickMultiplier = _httpContextAccessor.HttpContext.Session.GetDouble("YinClickMultiplier");
            var yangClickMultiplier = _httpContextAccessor.HttpContext.Session.GetDouble("YangClickMultiplier");
            var yinDiscount = _httpContextAccessor.HttpContext.Session.GetDouble("YinDiscount");
            var yangDiscount = _httpContextAccessor.HttpContext.Session.GetDouble("YangDiscount");
            var yinClickPowerPercentage = _httpContextAccessor.HttpContext.Session.GetDouble("YinClickPowerPercentage");
            var yangClickPowerPercentage = _httpContextAccessor.HttpContext.Session.GetDouble("YangClickPowerPercentage");
            

            if (yinPoints.HasValue) game.Yin.Points = yinPoints.Value;
            if (yangPoints.HasValue) game.Yang.Points = yangPoints.Value;
            if (yinClickPower.HasValue) game.Yin.ClickPower = yinClickPower.Value;
            if (yangClickPower.HasValue) game.Yang.ClickPower = yangClickPower.Value;
            if (yinCritChance.HasValue) game.Yin.CritChance = yinCritChance.Value;
            if (yangCritChance.HasValue) game.Yang.CritChance = yangCritChance.Value;
            if (yinClickMultiplier.HasValue) game.Yin.ClickMultiplier = yinClickMultiplier.Value;
            if (yangClickMultiplier.HasValue) game.Yang.ClickMultiplier = yangClickMultiplier.Value;
            if (yinDiscount.HasValue) game.Yin.Discount = yinDiscount.Value;
            if (yangDiscount.HasValue) game.Yang.Discount = yangDiscount.Value;
            if (yinClickPowerPercentage.HasValue) game.Yin.ClickPowerPercentage = yinClickPowerPercentage.Value;
            if (yangClickPowerPercentage.HasValue) game.Yang.ClickPowerPercentage = yangClickPowerPercentage.Value;

            return game;
        }

        public async Task SaveGame(BaseGame game)
        {
            if (_httpContextAccessor.HttpContext?.Session == null) return;

            _httpContextAccessor.HttpContext.Session.SetDouble("YinPoints", game.Yin.Points);
            _httpContextAccessor.HttpContext.Session.SetDouble("YangPoints", game.Yang.Points);
            _httpContextAccessor.HttpContext.Session.SetDouble("YinClickPower", game.Yin.ClickPower);
            _httpContextAccessor.HttpContext.Session.SetDouble("YinClickMultiplier", game.Yin.ClickMultiplier);
            _httpContextAccessor.HttpContext.Session.SetDouble("YangClickPower", game.Yang.ClickPower);
            _httpContextAccessor.HttpContext.Session.SetDouble("YangClickMultiplier", game.Yang.ClickMultiplier);
            _httpContextAccessor.HttpContext.Session.SetDouble("YinCritChance", game.Yin.CritChance);
            _httpContextAccessor.HttpContext.Session.SetDouble("YangCritChance", game.Yang.CritChance);
            _httpContextAccessor.HttpContext.Session.SetDouble("YinDiscount", game.Yin.Discount);
            _httpContextAccessor.HttpContext.Session.SetDouble("YangDiscount", game.Yang.Discount);
            _httpContextAccessor.HttpContext.Session.SetDouble("YinClickPowerPercentage", game.Yin.ClickPowerPercentage);
            _httpContextAccessor.HttpContext.Session.SetDouble("YangClickPowerPercentage", game.Yang.ClickPowerPercentage);
        }
    }
}