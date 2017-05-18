using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Center;
using poker.PokerGame;
using poker.Players;

namespace acceptanceTest
{
    class Service : IService
    {
        private List<Player> players;
        private List<IGame> games;

        public Service()
        {
            players = new List<Player>();
            games = new List<IGame>();
        }
        public bool call(IGame game, GamePlayer player)
        {
            throw new NotImplementedException();
        }

        public bool check(IGame game, GamePlayer player)
        {
            throw new NotImplementedException();
        }

        public IGame CreateGame(GamePreferences gp)
        {
            throw new NotImplementedException();
        }

        public bool EditPlayer(string username, string type, string newValue)
        {
            throw new NotImplementedException();
        }

        public List<IGame> FindAllGamesCanJoin(string username)
        {
            throw new NotImplementedException();
        }

        public List<IGame> FindAllGamesCanSpectate(string username)
        {
            throw new NotImplementedException();
        }

        public List<IGame> findGamesByPlayerName(string username)
        {
            throw new NotImplementedException();
        }

        public List<IGame> findGamesByPotSize(int pot)
        {
            throw new NotImplementedException();
        }

        public List<IGame> findGamesByPreference(GamePreferences pref)
        {
            throw new NotImplementedException();
        }

        public bool fold(IGame game, GamePlayer player)
        {
            throw new NotImplementedException();
        }

        public GamePlayer JoinGame(IGame game, string username, int amount)
        {
            throw new NotImplementedException();
        }

        public bool LeaveGame(IGame game, string username)
        {
            throw new NotImplementedException();
        }

        public bool Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool raise(IGame game, GamePlayer player, int amount)
        {
            throw new NotImplementedException();
        }

        public Player Register(string username, string password, string email)
        {
            throw new NotImplementedException();
        }

        public bool ReplayGame(IGame game, string username)
        {
            throw new NotImplementedException();
        }

        public bool SetBuyInPolicy(IGame game, int minBuyIn, int maxBuyIn)
        {
            throw new NotImplementedException();
        }

        public bool SetChipPoicy(IGame game, int amount)
        {
            throw new NotImplementedException();
        }

        public bool SetGamePrivacy(IGame game, bool privacy)
        {
            throw new NotImplementedException();
        }

        public bool SetGameTypePolicy(IGame game, string Policy)
        {
            throw new NotImplementedException();
        }

        public bool SetMinimumBet(IGame game, int amount)
        {
            throw new NotImplementedException();
        }

        public bool SpectateGame(IGame game, string username)
        {
            throw new NotImplementedException();
        }

        public bool StartGame(IGame game)
        {
            throw new NotImplementedException();
        }

        public bool FinishGame(IGame game)
        {
            throw new NotImplementedException();
        }

        public bool DefinePlayersInTable(IGame game, int minPlayers, int maxPlayers)
        {
            throw new NotImplementedException();
        }

        public bool MessageToChat(IGame game, string username, string msg)
        {
            throw new NotImplementedException();
        }

        public bool Whisper(IGame game, string from, string to, string msg)
        {
            throw new NotImplementedException();
        }
    }
}
