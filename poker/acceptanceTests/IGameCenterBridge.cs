using acceptanceTests.testObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace acceptanceTests
{
    interface IGameCenterBridge
    {
        Game CreateGame(Player player, Preferences prefs);

        //------------------Game Search----------------------

        List<Game> SearchGameByPlayerName(Player player);

        List<Game> SearchGameByPotSize(int potSize);

        List<Game> SearchGamesByPreferences(Preferences prefs);

        //---------------------------------------------------

        List<Game> DisplayAvailablePokerGames(Player player);

        List<Game> DisplayActiveGamesToSpectate(Player player);

        bool LogIn(String userName, String password);

        bool Register(Player player);

        Game ReplayGame(Game game);
    }
}
