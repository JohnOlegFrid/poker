using System;
using System.Collections.Generic;
using poker.Center;
using poker.PokerGame.Moves;
using poker.PokerGame.Exceptions;
using poker.Cards;
using Newtonsoft.Json;
using System.Linq;
using poker.Players;

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
        [JsonProperty]
        private Hand board;
        private int roundNumber;
        bool secondRunOnRound; // if we finish one lap of all the players
        GamePlayer firstPlayOnRound = null;
        [JsonProperty]
        private List<GamePlayer> winners;

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

        public List<GamePlayer> Winners { get { return winners; } set { winners = value; } }

        public bool Join(int chair, GamePlayer p)
        {
            for (int i = 0; i < gamePreferences.MaxPlayers; i++)
            {
                if ((ChairsInGame[i] != null) && (ChairsInGame[i].Equals(p))) //a player can't join a game twice.
                    return false;
            }
            if (ChairsInGame[chair] != null)
                return false;
            if (p.Money < gamePreferences.MinBuyIn || p.Money > gamePreferences.MaxBuyIn)
                return false;
            ChairsInGame[chair] = p;
            p.ChairNum = chair;
            p.SetFold(true);
            gameLog.Add(p.Player.Username + " joined the game.");
            currentPlayers++;
            return true;
        }

        public void LeaveGame(GamePlayer p)
        {
            if (active)
            {
                p.WantToExit = true;
            }
            else
            {
                ChairsInGame[p.ChairNum] = null;
                gameLog.Add(p.GetUsername() + " leaved the game");
            }
            
        }

        public bool IsActive()
        {
            return Active;
        }

        public void FinishGame()
        {
            gameLog.Add("Game is finished.");
            Active = false;
            activePlayer = null;
            FindWinners();
            GiveMoneyToWiners();
            HandleStatistics.updateStats(GetListActivePlayers());
            ThrowLeavedPlayers();
        }

        private void ThrowLeavedPlayers()
        {
            List<GamePlayer> gpList = GetListActivePlayers();
            foreach (GamePlayer gp in gpList)
                if (gp.WantToExit)
                    LeaveGame(gp);
        }

        private void GiveMoneyToWiners()
        {
            if (winners.Count == 1)
                gameLog.Add("And The Winner Is:");
            else
                gameLog.Add("And The Winners Are:");
            foreach(GamePlayer gp in winners)
            {
                gp.Money += this.pot / winners.Count;
                gameLog.Add(gp.GetUsername() + "! Won " + this.pot / winners.Count + "$");
            }
        }

        private void FindWinners()
        {
            List<GamePlayer> playersInGame = GetListActivePlayers();
            List<GamePlayer> playersThatFinishGame = playersInGame.FindAll(gp => !gp.IsFold());
            playersThatFinishGame.ForEach(gp => gp.Hand += board); // add the board cards to finish players
            Hand bestHand = FindBestHand(playersThatFinishGame);
            Winners = playersThatFinishGame.FindAll(gp => gp.Hand == bestHand);
        }

        private static Hand FindBestHand(List<GamePlayer> playersThatFinishGame)
        {
            Hand BestHand = playersThatFinishGame[0].Hand;
            foreach (GamePlayer gp in playersThatFinishGame)
            {
                if (gp.Hand > BestHand)
                {
                    BestHand = gp.Hand;
                }
            }
            return BestHand;
        }

        public void PlaceBlinds()
        {
            smallBlind = activePlayer;
            bigBlind = GetNextPlayer();
            Move move;
            if (smallBlind != null)
            {
                move = smallBlind.Raise(new Raise(gamePreferences.SmallBlind, smallBlind));
                lastMove = move;
            }

            if (bigBlind != null)
            {
                move = bigBlind.Raise(new Raise(gamePreferences.BigBlind, bigBlind));
                lastMove = move;
            }
        }

        public void StartGame()
        {
            if (currentPlayers >= gamePreferences.GetMinPlayers())
            {
                gameLog.Add("Starting game.");
                InitPlayers();
                deck = Deck.CreateFullDeck();
                deck.Shuffle();
                Active = true;
                activePlayer = GetFirstPlayer();
                if (!this.debug)
                    PlaceBlinds();
                DealCardsToPlayers();              
                this.pot = gamePreferences.SmallBlind + gamePreferences.BigBlind;
                this.highestBet = gamePreferences.BigBlind;
                board = new Hand();
                roundNumber = 0;
                secondRunOnRound = false;
                firstPlayOnRound = activePlayer;
                this.Winners = null;
            }
            else
                gameLog.Add("Not enough players to start");
        }

        private void InitPlayers()
        {
            List<GamePlayer> gpList = GetListActivePlayers();
            foreach (GamePlayer gp in gpList)
                gp.InitPlayer();
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
            if (GetActivePlayer() == null || !active)
                return;
            Move currentMove = GetActivePlayer().Play();
            PlayMove(currentMove);
            MoveToNextPlayer();
        }

        public void PlayMove(Move currentMove)
        {
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
            if (lastMove != null && lastMove.Name == "Raise" && currentMove.Player.CurrentBet < lastMove.Player.CurrentBet)
                throw new IllegalMoveException("Error!, " + currentMove.Player + " cant do " + currentMove.Name + " you need at least " + lastMove.Amount);
            if (currentMove.Player.CurrentBet < this.highestBet)
                throw new IllegalMoveException("Error!, " + currentMove.Player + " cant " + currentMove.Name + " you need to bet at least " + this.highestBet);
            if (currentMove.Amount == 0)
                return;
            if (currentMove.Name == "Raise" && currentMove.Amount < gamePreferences.BigBlind)
                throw new IllegalMoveException("Error!, " + currentMove.Player + " can raise at least " + gamePreferences.BigBlind);
            return;
        }

        public void NextRound()
        {
            roundNumber++;
            if (IsGameFinish())
            {
                FinishGame();
                return;
            }
            gameLog.Add("Starting round " + roundNumber);
            secondRunOnRound = false;
            if (roundNumber == 1)
            {
                //burn card + deal 3 card to board
                deck.Take(1); // burn card
                board.Add(deck.Take(3));
            }
            else
            {
                //burn card + deal more one card to board
                deck.Take(1); // burn card
                board.Add(deck.Take(1));
            }
            activePlayer = lastMove.Player; // move the active for the last player that play
            firstPlayOnRound = null; // this is new round
            activePlayer = GetNextPlayer();
            firstPlayOnRound = activePlayer;
        }

        private bool IsGameFinish()
        {
            if (roundNumber == 4)
                return true;
            List<GamePlayer> gpList = GetListActivePlayers();
            if (gpList.FindAll(gp => !gp.IsFold()).Count == 1)
                return true;
            return false;
        }

        private void MoveToNextPlayer()
        {
            if (IsGameFinish())
            {
                FinishGame();
                return;
            }
            activePlayer = GetNextPlayer();
            if (activePlayer == null)
                NextRound();
            else if (activePlayer.WantToExit)
            {
                activePlayer.NextMove = new Fold(activePlayer);
                NextTurn();
            }
        }

        private void AddMoveToLog(Move move)
        {
            gameLog.Add(move.ToString());
        }

        public GamePlayer GetNextPlayer()
        {
            int chair;
            if (activePlayer == null)
                if (lastMove == null || lastMove.Player != null)
                    chair = -1;
                else
                    chair = lastMove.Player.ChairNum;
            else
                chair = activePlayer.ChairNum;
            if (chair == ChairsInGame.Length - 1)
                chair = -1;
            for (int i = chair + 1; i != chair; i = (i + 1) % ChairsInGame.Length)
            {
                if (ChairsInGame[i] != null && !secondRunOnRound && firstPlayOnRound != null 
                    && ChairsInGame[i].Player.Equals(firstPlayOnRound.Player))
                    secondRunOnRound = true;
                if (ChairsInGame[i] != null && !ChairsInGame[i].IsFold() 
                    && (!(secondRunOnRound  && ChairsInGame[i].CurrentBet == highestBet)))
                    return ChairsInGame[i];
            }
            return FirstPlayerWithLowerPot();
        }

        private GamePlayer FirstPlayerWithLowerPot()
        {
            int chair = activePlayer.ChairNum;
            for (int i = chair + 1; i != chair; i = (i + 1) % ChairsInGame.Length)
            {
                if (chairsInGame[i] != null &&  !chairsInGame[i].IsFold() && chairsInGame[i].CurrentBet < highestBet)
                    return chairsInGame[i];
            }
            return null;
        }

        public GamePlayer GetFirstPlayer()
        {
            return GetNextPlayer();
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

        public GamePlayer[] GetChairs()
        {
            return chairsInGame;
        }

        public void DealCardsToPlayers()
        {
            List<GamePlayer> activePlayers = GetListActivePlayers();
            foreach (GamePlayer p in activePlayers)
            {
                p.Hand.Add(deck.Take(2));
            }
        }

    }
}
