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
        private GamePlayer[] playersInGame;
        private List<Player> spectators;
        private bool active;
        private bool finished;
        private List<string> gameLog;
        private GamePreferences gamePreferences;

        public TexasGame(GamePreferences gp)
        {
            this.gamePreferences = gp;
            playersInGame = new GamePlayer[this.gamePreferences.MaxPlayers];
            spectators = new List<Player>();
            active = false;
            finished = false;
            gameLog = new List<string>();
        }

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
            List<int> ans = new List<int>();
            if (!finished)
            {
                for (int i = 0; i < gamePreferences.MaxPlayers; i++)
                    if (playersInGame[i] == null)
                        ans.Add(i);
            }
            return ans;
        }

        public bool join(int amount, int chair, GamePlayer p)
        {
            for(int i=0; i<gamePreferences.MaxPlayers;i++)
            {
                if ((playersInGame[i]!=null) && (playersInGame[i].Equals(p))) //a player can't join a game twice.
                    return false;
            }
            if (playersInGame[chair] != null)
                return false;
            if (amount < gamePreferences.MinBuyIn || amount > gamePreferences.MaxBuyIn)
                return false;
            if (amount > p.Money)
                return false;
            playersInGame[chair] = p;
            gameLog.Add(p.Player.Username + " joined the game.");
            return true;
        }

        public bool isActive()
        {
            return Active;
        }

        public void finishGame()
        {
            gameLog.Add("Game is finished.");
            Active = false;
            finished = true;
        }

        public void startGame()
        {
            gameLog.Add("Starting game.");
            Active = true;
        }

        public List<string> replayGame()
        {
                gameLog.Add("game replayed");
                return gameLog;
        }

        public bool isFinished()
        {
            return finished;
        }
        

        public bool isAllowSpectating()
        {
            return gamePreferences.AllowSpectating;
        }
    }
}
