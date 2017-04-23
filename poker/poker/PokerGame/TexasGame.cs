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
        private int currentPlayers;
        private List<Player> spectators;
        private bool active;
        private List<string> gameLog;
        private List<string> errorLog;
        private GamePreferences gamePreferences;
        private GamePlayer activePlayer;
        private int pot;
        private int highestBet;
        private Move lastMove;
        private GamePlayer smallBlind;
        private GamePlayer bigBlind;
        private GamePlayer dealer;//the first player to get the cards in each hand.

        public TexasGame(GamePreferences gp)
        {
            this.gamePreferences = gp;
            playersInGame = new GamePlayer[this.gamePreferences.MaxPlayers];
            spectators = new List<Player>();
            active = false;
            gameLog = new List<string>();
            errorLog = new List<string>();
            pot = 0;
            highestBet = 0;
            currentPlayers = 0;
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

        public List<int> AskToJoin() //the method returns list of free chairs , why its AskToJoin? doesn't clear enough.
        {
            List<int> ans = new List<int>();
            if (active)
            {
                for (int i = 0; i < gamePreferences.MaxPlayers; i++)
                    if (playersInGame[i] == null)
                        ans.Add(i);
            }
            return ans;
        }

        public bool Join(int amount, int chair, GamePlayer p)
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
            currentPlayers++;
            return true;
        }

        public bool IsActive()
        {
            return Active;
        }

        public void FinishGame()
        {
            gameLog.Add("Game is finished.");
            Active = false;
        }

        public void placeBlinds()
        {
            smallBlind = activePlayer;
            bigBlind = GetNextPlayer();
            smallBlind.Raise(new Raise(gamePreferences.SmallBlind, smallBlind));
            bigBlind.Raise(new Raise(gamePreferences.BigBlind, bigBlind));
        }

        public void StartGame()
        {
            if (currentPlayers >= 2)
            {
                gameLog.Add("Starting game.");
                Active = true;
                activePlayer = GetFirstPlayer();
                this.pot = 0;
                this.highestBet = 0;
                placeBlinds();
            }
            else
                gameLog.Add("Not enough players to start");
        }

        public List<string> ReplayGame()
        {
                gameLog.Add("game replayed");
                return gameLog;
        }
        
        public bool IsAllowSpectating()
        {
            return gamePreferences.AllowSpectating;
        }

        public GamePlayer GetActivePlayer()
        {
            if (activePlayer == null)
                return activePlayer;
            return playersInGame[this.activePlayer.ChairNum];
        }
        
        public void NextTurn()
        {
            if (GetActivePlayer() == null)
                return;
            Move currentMove = GetActivePlayer().Play();
            try
            {
                ValidateMoveIsLeagal(currentMove);
            }
            catch (PokerExceptions pe)
            {
                CancelMove(currentMove);
                errorLog.Add(pe.Message);
                return;
            }
            AddMoveToPot(currentMove);
            AddMoveToLog(currentMove);
            MoveToNextPlayer();
            lastMove = currentMove;
        }

        private void CancelMove(Move currentMove)
        {
            if (currentMove != null)
                currentMove.Player.CancelMove(currentMove);
        }

        private void AddMoveToPot(Move currentMove)
        {       
            this.pot += currentMove.Amount;
            if (currentMove.Player.CurrentBet > this.highestBet)
                this.highestBet = currentMove.Player.CurrentBet;
        }

        private void ValidateMoveIsLeagal(Move currentMove)
        {
            //TODO 3 GAME MODE
            if(currentMove == null)
                throw new IllegalMoveException("Error!, you cant do this move");
            if (currentMove.Name == "Fold")
                return;
            if(lastMove != null && lastMove.Name == "Raise" && currentMove.Amount < lastMove.Amount)
                throw new IllegalMoveException("Error!, " + currentMove.Player +" cant do " +currentMove.Name+ " you need at least "+ lastMove.Amount);
            if (currentMove.Player.CurrentBet < this.highestBet)
                throw new IllegalMoveException("Error!, " + currentMove.Player + " cant " + currentMove.Name + " you need to bet at least " +this.highestBet);
            if (currentMove.Amount == 0)
                return;
            if(currentMove.Amount < gamePreferences.BigBlind)
                throw new IllegalMoveException("Error!, " + currentMove.Player + " can raise at least " + gamePreferences.BigBlind);
            return;
        }

        public void NextRound()
        {
            activePlayer = GetFirstPlayer();
            highestBet = 0;
            //TODO add this function logic , with deck
        }

        private void MoveToNextPlayer()
        {
            activePlayer = GetNextPlayer();
        }

        private void AddMoveToLog(Move move)
        {
            gameLog.Add(move.ToString());
        }

        // return null if all the players play
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

        // return null if no more active players
        public GamePlayer GetFirstPlayer()
        {
            for(int i = 0; i < this.playersInGame.Length; i++)
            {
                if (playersInGame[i] != null && !playersInGame[i].IsFold())
                    return playersInGame[i];
            }
            return null;
        }

        public List<Player> GetListActivePlayers()
        {
            List<Player> ans = new List<Player>();
            if (playersInGame.Length == 0 || !Active) return null; // no active players for that game
            foreach(GamePlayer p in playersInGame)
            {
                if(p!=null)
                    ans.Add(p.Player);
            }
            return ans;
        }

        public override bool Equals (Object obj)
        {
            if (!(obj is TexasGame))
                return false;
            TexasGame tg = (TexasGame)obj;
            if (tg.playersInGame != playersInGame)
                return false;
            return true;
        }

        public void spectateGame(Player p)
        {
            if (IsActive() && IsAllowSpectating() && !spectators.Contains(p))
            {
                spectators.Add(p);
                p.CurrentlyWatching.Add(this);
            }
        }

        public void stopWatching(Player p)
        {
            if (spectators.Contains(p))
            {
                spectators.Remove(p);
                p.CurrentlyWatching.Remove(this);
            }
        }

        public List<Player> getAllSpectators()
        {
            return spectators;
        }
    }
}
