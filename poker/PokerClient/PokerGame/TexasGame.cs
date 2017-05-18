using System;
using System.Collections.Generic;
using poker.PokerGame.Moves;
using poker.PokerGame.Exceptions;
using PokerClient.Center;
using PokerClient.Cards;

namespace poker.PokerGame
{
    public class TexasGame : IGame
    {
        private GamePlayer[] chairsInGame;
        private int currentPlayers;
        private bool active;
        private List<string> gameLog;
        private GamePreferences gamePreferences;
        private GamePlayer activePlayer;
        private int pot;
        private int highestBet;
        private Move lastMove;
        private GamePlayer smallBlind;
        private GamePlayer bigBlind;
        private Hand board;

        public TexasGame(GamePlayer[] chairsInGame, int currentPlayers, bool active, List<string> gameLog,
            GamePreferences gamePreferences, GamePlayer activePlayer, int pot, int highestBet, Move lastMove,
            GamePlayer smallBlind, GamePlayer bigBlind, Hand hand)
        {
            this.ChairsInGame = chairsInGame;
            this.currentPlayers = currentPlayers;
            this.Active = active;
            this.gameLog = gameLog;
            this.GamePreferences = gamePreferences;
            this.activePlayer = activePlayer;
            this.Pot = pot;
            this.highestBet = highestBet;
            this.lastMove = lastMove;
            this.smallBlind = smallBlind;
            this.bigBlind = bigBlind;
            this.Board = hand;

        }

        public override string ToString()
        {
            return "Active:" + Active.ToString() + " Pot:" + Pot;
        }


        public GamePreferences GamePreferences { get { return gamePreferences; } set { gamePreferences = value; } }

        public GamePlayer[] ChairsInGame { get { return chairsInGame; } set { chairsInGame = value; } }

        public Hand Board { get { return board; } set { board = value; } }

        public bool Active { get { return active; } set { active = value; } }

        public int Pot { get { return pot; } set { pot = value; } }

        public bool IsActive()
        {
            return Active;
        }

        public bool IsAllowSpectating()
        {
            return GamePreferences.AllowSpectating;
        }

        public GamePlayer GetActivePlayer()
        {
            if (activePlayer == null)
                return activePlayer;
            return ChairsInGame[this.activePlayer.ChairNum];
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
            if (currentMove.Amount < GamePreferences.BigBlind)
                throw new IllegalMoveException("Error!, " + currentMove.Player + " can raise at least " + GamePreferences.BigBlind);
            return;
        }


        public List<GamePlayer> GetListActivePlayers()
        {
            List<GamePlayer> ans = new List<GamePlayer>();
            if (ChairsInGame.Length == 0) return null; // no active players for that game
            foreach (GamePlayer p in ChairsInGame)
            {
                if (p != null)
                    ans.Add(p);
            }
            return ans;
        }
    }
}
