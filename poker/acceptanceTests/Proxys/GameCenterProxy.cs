using System;
using System.Collections.Generic;
using System.Text;
using acceptanceTests.testObjects;

namespace acceptanceTests.Proxys
{
    class GameCenterProxy : IGameCenterBridge
    {
        public Game CreateGame(Player player, Preferences prefs)
        {
            return null;
        }

        public List<Game> DisplayActiveGamesToSpectate(Player player)
        {
            return null;
        }

        public List<Game> DisplayAvailablePokerGames(Player player)
        {
            return null;
        }

        public bool LogIn(string userName, string password)
        {
            return false;
        }

        public bool Register(Player player)
        {
            return false;
        }

        public Game ReplayGame(Game game)
        {
            return null;
        }

        public List<Game> SearchGameByPlayerName(Player player)
        {
            return null;
        }

        public List<Game> SearchGameByPotSize(int potSize)
        {
            return null;
        }

        public List<Game> SearchGamesByPreferences(Preferences prefs)
        {
            return null;
        }
    }
}
