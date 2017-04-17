using System;
using System.Collections.Generic;
using System.Text;
using acceptanceTests.testObjects;

namespace acceptanceTests
{
    interface IGameBridge
    {

        //---------Game Preferences--------------------------

        bool SetGameTypePolicy(String Policy);

        bool SetBuyInPolicy(int buyIn);

        bool SetChipPoicy(int amount);

        bool SetMinimumBet(int amount);

        bool DefinePlayersInTable(int minPlayers, int maxPlayers);

        bool setGamePrivacy(bool privacy);

        //---------------------------------------------------

        bool JoinGame(Player player);

        bool SpectateGame(Player player);

        bool LeaveGame(Player player);

        bool SaveFavoriteTurns(Game game);




    }

}
