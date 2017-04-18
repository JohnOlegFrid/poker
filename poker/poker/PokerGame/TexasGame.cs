using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Center;
using poker.Players;

namespace poker.PokerGame
{
    public class TexasGame : IGame
    {
        private int maxPlayers;
        private Player[] playersInGame;
        private List<Player> spectators;
        private bool active;

        public bool Active
        {
            get
            {
                return active;
            }

            set
            {
                active = value;
            }
        }

        public List<int> askToJoin()
        {
            throw new NotImplementedException();
        }

        public bool join(int amount)
        {
            throw new NotImplementedException();
        }

        public bool isActive()
        {
            return Active;
        }

        public void finishGame()
        {
            Active = false;
        }

        public void startGame()
        {
            Active = true;
        }
        private GamePreferences gamePreferences;

        public TexasGame(GamePreferences gamePreferences)
        {
            this.gamePreferences = gamePreferences;
        }

        public bool isAllowSpectating()
        {
            return gamePreferences.AllowSpectating;
        }
    }
}
