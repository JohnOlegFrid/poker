using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Center;
using poker.Players;
using poker.PokerGame.Moves;
using poker.PokerGame.Exceptions;
using poker.Cards;

namespace poker.PokerGame
{
    public class TexasGame : IGame
    {
        private GamePlayer[] chairsInGame;
        private int currentPlayers;
        private List<Player> spectators;
        private bool started;
        private bool finished;
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
        public bool debug = false;
        private Deck deck;
        private Card[] board;


        public TexasGame(GamePreferences gp)
        {
            this.gamePreferences = gp;
            chairsInGame = new GamePlayer[this.gamePreferences.MaxPlayers];
            spectators = new List<Player>();
            started = false;
            finished = false;
            gameLog = new List<string>();
            errorLog = new List<string>();
            pot = 0;
            highestBet = 0;
            currentPlayers = 0;
        }

        public List<int> GetFreeChairs() //the method returns list of free chairs , why its AskToJoin? doesn't clear enough.
        {
            List<int> ans = new List<int>();
            if (IsActive())
            {
                for (int i = 0; i < gamePreferences.MaxPlayers; i++)
                    if (chairsInGame[i] == null)
                        ans.Add(i);
            }
            return ans;
        }

        public bool Join(int amount, int chair, GamePlayer p)
        {
            for (int i = 0; i < gamePreferences.MaxPlayers; i++)
            {
                if ((chairsInGame[i] != null) && (chairsInGame[i].Equals(p))) //a player can't join a game twice.
                    return false;
            }
            if (chairsInGame[chair] != null)
                return false;
            if (amount < gamePreferences.MinBuyIn || amount > gamePreferences.MaxBuyIn)
                return false;
            if (amount > p.Money)
                return false;
            chairsInGame[chair] = p;
            p.ChairNum = chair;
            gameLog.Add(p.Player.Username + " joined the game.");
            currentPlayers++;
            return true;
        }

        public bool IsActive()
        {
            return !finished;
        }

        public void FinishGame()
        {
            gameLog.Add("Game is finished.");
            finished = true;
        }

        public void PlaceBlinds()
        {
            smallBlind = activePlayer;
            bigBlind = GetNextPlayer();
            if (smallBlind != null)
                smallBlind.Raise(new Raise(gamePreferences.SmallBlind, smallBlind));
            if (bigBlind != null)
                bigBlind.Raise(new Raise(gamePreferences.BigBlind, bigBlind));
        }

        public void StartGame()
        {
            if (currentPlayers >= gamePreferences.GetMinPlayers())
            {
                gameLog.Add("Starting game.");
                deck = Deck.CreateFullDeck();
                started = true;

                /*
                 * --big and small put blinds
                 * --dealing cards to players
                 * first round to table {call/raise/fold}
                 * chack if raise {do call round to}
                 * burn card +deal 3 card to board
                 * seccond round to table {call/raise/fold}
                 * chack if raise {do call round to}
                 * burn card +deal 4's card to board
                 * third round to table {call/raise/fold}
                 * chack if raise {do call round to}
                 * burn card +deal 5's card to board
                 * fourth round to table {call/raise/fold}
                 * chack if raise {do call round to}
                 * reveal all cards
                 * calculate winner
                 * deal money to winners
                 *
                 */
                if (!this.debug)
                    PlaceBlinds();
                DealCardsToPlayers();
                activePlayer = GetFirstPlayer();
                this.pot = gamePreferences.SmallBlind + gamePreferences.BigBlind;
                this.highestBet = gamePreferences.BigBlind;
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
            return chairsInGame[this.activePlayer.ChairNum];
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
            if (currentMove == null)
                throw new IllegalMoveException("Error!, you cant do this move");
            if (currentMove.Name == "Fold")
                return;
            if (lastMove != null && lastMove.Name == "Raise" && currentMove.Amount < lastMove.Amount)
                throw new IllegalMoveException("Error!, " + currentMove.Player + " cant do " + currentMove.Name + " you need at least " + lastMove.Amount);
            if (currentMove.Player.CurrentBet < this.highestBet)
                throw new IllegalMoveException("Error!, " + currentMove.Player + " cant " + currentMove.Name + " you need to bet at least " + this.highestBet);
            if (currentMove.Amount == 0)
                return;
            if (currentMove.Amount < gamePreferences.BigBlind)
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
            for (int i = 1; i < gamePreferences.MaxPlayers - chair; i++)
            {
                if (chair + 1 < gamePreferences.MaxPlayers && chairsInGame[chair + i] != null && !chairsInGame[chair + i].IsFold())
                    return chairsInGame[chair + i];

            }
            return null;
        }

        // return null if no more active players
        public GamePlayer GetFirstPlayer()
        {
            for (int i = 0; i < this.chairsInGame.Length; i++)
            {
                if (chairsInGame[i] != null && !chairsInGame[i].IsFold())
                    return chairsInGame[i];
            }
            return null;
        }

        public List<GamePlayer> GetListActivePlayers()
        {
            List<GamePlayer> ans = new List<GamePlayer>();
            if (chairsInGame.Length == 0 || !IsActive()) return null; // no active players for that game
            foreach (GamePlayer p in chairsInGame)
            {
                if (p != null)
                    ans.Add(p);
            }
            return ans;
        }


        public override bool Equals(Object obj)
        {
            if (!(obj is TexasGame))
                return false;
            TexasGame tg = (TexasGame)obj;
            if (tg.chairsInGame != chairsInGame)
                return false;
            return true;
        }

        public void SpectateGame(Player p)
        {
            if (IsActive() && IsAllowSpectating() && !spectators.Contains(p))
            {
                spectators.Add(p);
                p.CurrentlyWatching.Add(this);
            }
        }

        public void StopWatching(Player p)
        {
            if (spectators.Contains(p))
            {
                spectators.Remove(p);
                p.CurrentlyWatching.Remove(this);
            }
        }

        public List<Player> GetAllSpectators()
        {
            return spectators;
        }

        public void DealCardsToPlayers()
        {
            List<GamePlayer> activePlayers = GetListActivePlayers();
            foreach (GamePlayer p in activePlayers)
            {
                p.CardsPlayer[1] = deck.Take(1)[0];
                p.CardsPlayer[2] = deck.Take(1)[0];
            }
        }

        int IGame.highestBet()
        {
            return highestBet;
        }

        public void setHighestBet(int bet)
        {
            highestBet = bet;
        }

        public void SetActivePlayer(GamePlayer player)
        {
            throw new NotImplementedException();
        }

        public int getPot()
        {
            throw new NotImplementedException();
        }

        public string getPolicy()
        {
            throw new NotImplementedException();
        }

        public int getMinBuyIn()
        {
            throw new NotImplementedException();
        }

        public int getMaxBuyIn()
        {
            throw new NotImplementedException();
        }

        public int getMinPlayer()
        {
            throw new NotImplementedException();
        }

        public int getMaxPlayer()
        {
            throw new NotImplementedException();
        }
    }
}
