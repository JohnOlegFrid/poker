using System;
using System.Collections.Generic;
using System.Text;
using acceptanceTests.testObjects;

namespace acceptanceTests.Proxys
{
    class GameProxy : IGameBridge
    {
        public Game game;

        public bool DefinePlayersInTable(int minPlayers, int maxPlayers)
        {
            return false;
        }

        public Game GetGame()
        {
            return null;
        }

        public void InitGame()
        {
            game = null;
        }

        public bool JoinGame(Player player)
        {
            return false;
        }

        public bool LeaveGame(Player player)
        {
            return false;
        }

        public bool SaveFavoriteTurns(Game game, Player player)
        {
            return false;
        }

        public bool SetBuyInPolicy(Game game, int buyIn)
        {
            return false;
        }

        public bool SetChipPoicy(Game game, int amount)
        {
            return false;
        }

        public bool SetGamePrivacy(Game game, bool privacy)
        {
            return false;
        }

        public bool SetGameTypePolicy(Game game, string Policy)
        {
            return false;
        }

        public bool SetMinimumBet(Game game, int amount)
        {
            return false;
        }

        public bool SpectateGame(Player player)
        {
            return false;
        }
    }
}
