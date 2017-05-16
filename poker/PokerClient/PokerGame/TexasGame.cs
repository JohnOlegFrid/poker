using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.PokerGame.Moves;
using poker.PokerGame.Exceptions;
using Newtonsoft.Json;
using PokerClient.Center;
using PokerClient.Players;

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

        public TexasGame(GamePlayer[] chairsInGame, int currentPlayers, bool active, List<string> gameLog,
            GamePreferences gamePreferences, GamePlayer activePlayer, int pot, int highestBet, Move lastMove,
            GamePlayer smallBlind, GamePlayer bigBlind)
        {
            this.chairsInGame = chairsInGame;
            this.currentPlayers = currentPlayers;
            this.active = active;
            this.gameLog = gameLog;
            this.gamePreferences = gamePreferences;
            this.activePlayer = activePlayer;
            this.pot = pot;
            this.highestBet = highestBet;
            this.lastMove = lastMove;
            this.smallBlind = smallBlind;
            this.bigBlind = bigBlind;
        }

        public override string ToString()
        {
            return "Active:" + active.ToString() + " Pot:" + pot;
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

        public List<int> getFreeChairs() //the method returns list of free chairs , why its AskToJoin? doesn't clear enough.
        {
            List<int> ans = new List<int>();
            if (active)
            {
                for (int i = 0; i < gamePreferences.MaxPlayers; i++)
                    if (chairsInGame[i] == null)
                        ans.Add(i);
            }
            return ans;
        }

        public bool IsActive()
        {
            return Active;
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


        public override bool Equals(Object obj)
        {
            if (!(obj is TexasGame))
                return false;
            TexasGame tg = (TexasGame)obj;
            if (tg.chairsInGame != chairsInGame)
                return false;
            return true;
        }
    }
}
