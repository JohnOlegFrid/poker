using System;
using System.Collections.Generic;
using System.Text;
using acceptanceTests.testObjects;

namespace acceptanceTests
{
    interface IGameBridge
    {

        //---------Game Preferences--------------------------

        bool SetGameTypePolicy(Game game, String Policy);

        bool SetBuyInPolicy(Game game, int buyIn);

        bool SetChipPoicy(Game game, int amount);

        bool SetMinimumBet(Game game, int amount);

        bool DefinePlayersInTable(int minPlayers, int maxPlayers);

        bool SetGamePrivacy(Game game, bool privacy);

        //---------------------------------------------------

        bool JoinGame(Player player);

        bool SpectateGame(Player player);

        bool LeaveGame(Player player);

        bool SaveFavoriteTurns(Game game, Player player);

        Game GetGame();

        void InitGame();


    }

}
