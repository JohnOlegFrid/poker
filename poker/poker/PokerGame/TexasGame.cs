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
using Newtonsoft.Json;

namespace poker.PokerGame
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TexasGame : IGame
    {
        [JsonProperty]
        private GamePlayer[] chairsInGame;
        [JsonProperty]
        private int currentPlayers;
        [JsonProperty]
        private bool active;
        [JsonProperty]
        private List<string> gameLog;
        private List<string> errorLog;
        [JsonProperty]
        private GamePreferences gamePreferences;
        [JsonProperty]
        private GamePlayer activePlayer;
        [JsonProperty]
        private int pot;
        [JsonProperty]
        private int highestBet;
        [JsonProperty]
        private Move lastMove;
        [JsonProperty]
        private GamePlayer smallBlind;
        [JsonProperty]
        private GamePlayer bigBlind;
        public bool debug = false;
        private Deck deck;
        private Card[] board;


        public TexasGame(GamePreferences gp)
        {
            this.gamePreferences = gp;
            ChairsInGame = new GamePlayer[this.gamePreferences.MaxPlayers];
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

        public GamePlayer[] ChairsInGame { get { return chairsInGame; } set { chairsInGame = value; } }



        public bool Join(int amount, int chair, GamePlayer p)
        {
            for (int i = 0; i < gamePreferences.MaxPlayers; i++)
            {
                if ((ChairsInGame[i] != null) && (ChairsInGame[i].Equals(p))) //a player can't join a game twice.
                    return false;
            }
            if (ChairsInGame[chair] != null)
                return false;
            if (amount < gamePreferences.MinBuyIn || amount > gamePreferences.MaxBuyIn)
                return false;
            if (amount > p.Money)
                return false;
            ChairsInGame[chair] = p;
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
                Active = true;

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
            return ChairsInGame[this.activePlayer.ChairNum];
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
                if (chair+1 < gamePreferences.MaxPlayers && ChairsInGame[chair + i] != null && !ChairsInGame[chair + i].IsFold())
                    return ChairsInGame[chair + i];

            }
            return null;
        }

        // return null if no more active players
        public GamePlayer GetFirstPlayer()
        {
            for(int i = 0; i < this.ChairsInGame.Length; i++)
            {
                if (ChairsInGame[i] != null && !ChairsInGame[i].IsFold())
                    return ChairsInGame[i];
            }
            return null;
        }

        public List<GamePlayer> GetListActivePlayers()
        {
            List<GamePlayer> ans = new List<GamePlayer>();
            if (ChairsInGame.Length == 0 ) return null; // no active players for that game
            foreach(GamePlayer p in ChairsInGame)
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
            if (tg.ChairsInGame != ChairsInGame)
                return false;
            return true;
        }


        public GamePlayer[] GetChairs()
        {
            return chairsInGame;
        }

        public void DealCardsToPlayers()
        {
            List<GamePlayer> activePlayers = GetListActivePlayers();
            foreach (GamePlayer p in activePlayers)
            {
                p.CardsPlayer[0] = deck.Take(1)[0];
                p.CardsPlayer[1] = deck.Take(1)[0];
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
