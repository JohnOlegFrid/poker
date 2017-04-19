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

        public bool JoinGame(Player player)
        {
            throw new NotImplementedException();
        }

        public bool LeaveGame(Player player)
        {
            throw new NotImplementedException();
        }

        public bool SaveFavoriteTurns(Game game)
        {
            throw new NotImplementedException();
        }

        public bool SetBuyInPolicy(int buyIn)
        {
            throw new NotImplementedException();
        }

        public bool SetChipPoicy(int amount)
        {
            throw new NotImplementedException();
        }

        public bool setGamePrivacy(bool privacy)
        {
            throw new NotImplementedException();
        }

        public bool SetGameTypePolicy(string Policy)
        {
            throw new NotImplementedException();
        }

        public bool SetMinimumBet(int amount)
        {
            throw new NotImplementedException();
        }

        public bool SpectateGame(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
