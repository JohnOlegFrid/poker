using poker.Center;
using poker.Players;
using poker.PokerGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace acceptanceTest
{
    interface IService
    {
        Player Register(string username, string password, string email);
        bool Login(String username, String password);
        bool EditPlayer(string username, string type, string newValue);
        IGame CreateGame(GamePreferences gp);
        GamePlayer JoinGame(IGame game, string username, int amount);
        bool SpectateGame(IGame game, string username);
        bool StartGame(IGame game);
        bool FinishGame(IGame game);
        bool LeaveGame(IGame game, string username);
        bool ReplayGame(IGame game, string username);//needs to be added to the log- who watched the replay.
        bool call(IGame game, GamePlayer player);//no need for amount parameter, game object should know the amount.
        bool raise(IGame game, GamePlayer player, int amount);
        bool check(IGame game, GamePlayer player);
        bool fold(IGame game, GamePlayer player);
        List<IGame> FindAllGamesCanJoin(string username);
        List<IGame> FindAllGamesCanSpectate(string username);
        List<IGame> findGamesByPlayerName(string username);
        List<IGame> findGamesByPotSize(int pot);
        List<IGame> findGamesByPreference(GamePreferences pref);
        bool SetGameTypePolicy(IGame game, String Policy);
        bool SetBuyInPolicy(IGame game, int minBuyIn, int maxBuyIn);
        bool SetChipPoicy(IGame game, int amount);
        bool SetMinimumBet(IGame game, int amount);
        bool DefinePlayersInTable(IGame game, int minPlayers, int maxPlayers);
        bool SetGamePrivacy(IGame game, bool privacy);
        bool MessageToChat(IGame game, string username, string msg);
        bool Whisper(IGame game, string from, string to, string msg);
    }
}
