using System;
using System.Collections.Generic;
using System.Text;
using acceptanceTests.testObjects;

namespace acceptanceTests.Proxys
{
    class GameProxy : IGameBridge
    {
        public bool DefinePlayersInTable(int minPlayers, int maxPlayers)
        {
            return false;
        }

        public bool JoinGame(Player player)
        {
            return false;
        }

        public bool LeaveGame(Player player)
        {
            return false;
        }

        public bool SaveFavoriteTurns(Game game)
        {
            return false;
        }

        public bool SetBuyInPolicy(int buyIn)
        {
            return false;
        }

        public bool SetChipPoicy(int amount)
        {
            return false;
        }

        public bool setGamePrivacy(bool privacy)
        {
            return false;
        }

        public bool SetGameTypePolicy(string Policy)
        {
            return false;
        }

        public bool SetMinimumBet(int amount)
        {
            return false;
        }

        public bool SpectateGame(Player player)
        {
            return false;
        }
    }
}
