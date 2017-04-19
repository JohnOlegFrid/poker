using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Center;
using poker.Players;
using poker.PokerGame.Moves;
using poker.PokerGame.Exceptions;

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
        private GamePlayer activePlayer;
        private int pot;
        private int highestBet;

        public TexasGame(GamePreferences gp)
        {
            this.gamePreferences = gp;
            playersInGame = new GamePlayer[this.gamePreferences.MaxPlayers];
            spectators = new List<Player>();
            active = false;
            finished = false;
            gameLog = new List<string>();
            pot = 0;
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
                if ((playersInGame[i] != null) && (playersInGame[i].Equals(p))) //a player can't join a game twice.
                    return false;
            }
            if (playersInGame[chair] != null)
                return false;
            if (amount < gamePreferences.MinBuyIn || amount > gamePreferences.MaxBuyIn)
                return false;
            if (amount > p.Money)
                return false;
            playersInGame[chair] = p;
            p.ChairNum = chair;
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
            activePlayer = GetFirstPlayer();
            this.pot = 0;
            this.highestBet = 0;
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

        public GamePlayer GetActivePlayer()
        {
            return playersInGame[this.activePlayer.ChairNum];
        }

        public void NextTurn()
        {
            if (GetActivePlayer() == null)
                return;
            Move currentMove = GetActivePlayer().Play();
            AddMoveToPot(currentMove);
            AddMoveToLog(currentMove);
            MoveToNextPlayer();
        }

        private void AddMoveToPot(Move currentMove)
        {
            ValidateMoveIsLeagal(currentMove);
            this.pot += currentMove.Amount;
            if (currentMove.Player.CurrentBet > this.highestBet)
                this.highestBet = currentMove.Player.CurrentBet;
        }

        private void ValidateMoveIsLeagal(Move currentMove)
        {
            //TODO 3 GAME MODE
            if (currentMove.Amount == 0)
                return;
            if(currentMove.Amount < gamePreferences.BigBlind)
                throw new IllegalMoveException("Error!, you can raise at least "+ gamePreferences.BigBlind);
            return;
        }

        private void MoveToNextPlayer()
        {
            activePlayer = GetNextPlayer();
        }

        private void AddMoveToLog(Move move)
        {
            gameLog.Add(move.ToString());
        }

        public GamePlayer GetNextPlayer()
        {
            int chair = activePlayer.ChairNum;
            for(int i=1; i<gamePreferences.MaxPlayers - chair; i++)
            {
                if (chair+1 < gamePreferences.MaxPlayers && playersInGame[chair + i] != null && !playersInGame[chair + i].IsFold())
                    return playersInGame[chair + i];

            }
            return null;
        }

        public GamePlayer GetFirstPlayer()
        {
            return playersInGame[0];
        }
    }
}
