using System;
using System.Collections.Generic;
using poker.PokerGame.Moves;
using poker.PokerGame.Exceptions;
using PokerClient.Center;
using PokerClient.Cards;
using PokerClient.Players;
using System.Linq;

namespace poker.PokerGame
{
    public class TexasGame : IGame
    {
        private GamePlayer[] chairsInGame;
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
        private List<GamePlayer> winners;

        public TexasGame(GamePlayer[] chairsInGame, bool active, List<string> gameLog,
            GamePreferences gamePreferences, GamePlayer activePlayer, int pot, int highestBet, Move lastMove,
            GamePlayer smallBlind, GamePlayer bigBlind, Hand hand, List<GamePlayer> winners)
        {
            this.ChairsInGame = chairsInGame;
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
            this.winners = winners;
        }

        public List<GamePlayer> Winners { get { return winners; } set { winners = value; } }

        public override string ToString()
        {
            return "Active:" + Active.ToString() + " Pot:" + Pot;
        }


        //public GamePreferences GamePreferences { get { return gamePreferences; } set { gamePreferences = value; } }

        public GamePlayer[] ChairsInGame { get { return chairsInGame; } set { chairsInGame = value; } }

        public Hand Board { get { return board; } set { board = value; } }

        public bool Active { get { return active; } set { active = value; } }

        public int Pot { get { return pot; } set { pot = value; } }

        public GamePreferences GamePreferences { get => gamePreferences; set => gamePreferences = value; }

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

        public GamePlayer[] GetChairsInGame()
        {
            return ChairsInGame;
        }

        public void SetChairsInGame(GamePlayer[] chairs)
        {
            ChairsInGame = chairs;
        }

        public List<string> GetGameLog()
        {
            return gameLog;
        }

        public bool IsPlayerActiveInGame(Player player)
        {
            return GetListActivePlayers().Any(gp => gp.Player.Equals(player));
        }

        public GamePlayer GetSmallBlind()
        {
            return smallBlind;
        }

        public GamePlayer GetBigBlind()
        {
            return bigBlind;
        }

        public Move GetLastMove()
        {
            return lastMove;
        }

        public int GetHigestBet()
        {
            return highestBet;
        }
    }
}
