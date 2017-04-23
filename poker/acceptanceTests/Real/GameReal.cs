using System;
using System.Collections.Generic;
using System.Text;
using acceptanceTests.testObjects;

namespace acceptanceTests.Real
{
    class GameReal : IGameBridge
    {
        public bool DefinePlayersInTable(int minPlayers, int maxPlayers)
        {
            throw new NotImplementedException();
        }

        public Game GetGame()
        {
            throw new NotImplementedException();
        }

        public void InitGame()
        {
            throw new NotImplementedException();
        }

        public bool JoinGame(Player player)
        {
            throw new NotImplementedException();
        }

        public bool LeaveGame(Player player)
        {
            throw new NotImplementedException();
        }

        public bool SaveFavoriteTurns(Game game, Player player)
        {
            throw new NotImplementedException();
        }

        public bool SetBuyInPolicy(Game game, int buyIn)
        {
            throw new NotImplementedException();
        }

        public bool SetChipPoicy(Game game, int amount)
        {
            throw new NotImplementedException();
        }


        public bool SetGamePrivacy(Game game, bool privacy)
        {
            throw new NotImplementedException();
        }

        public bool SetGameTypePolicy(Game game, string Policy)
        {
            throw new NotImplementedException();
        }

        public bool SetMinimumBet(Game game, int amount)
        {
            throw new NotImplementedException();
        }

        public bool SpectateGame(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
