using PokerClient.Players;
using poker.PokerGame.Exceptions;
using poker.PokerGame.Moves;
using System;
using PokerClient.Cards;

namespace poker.PokerGame
{
    public class GamePlayer
    {
        private Player player;
        private int chairNum;
        private int money;
        private bool isFold;
        private Move nextMove;
        private int currentBet;
        private Hand hand;
        private bool wantToExit;

        public GamePlayer(Player player, int chairNum, int money, bool isFold, Move nextMove, int currentBet, Hand hand, bool wantToExit)
        {
            this.player = player;
            this.chairNum = chairNum;
            this.money = money;
            this.IsFold = isFold;
            this.nextMove = nextMove;
            this.currentBet = currentBet;
            this.hand = hand;
            this.wantToExit = wantToExit;
        }

        public int Money { get { return money; } set { money = value; } }

        public int CurrentBet { get { return currentBet; } set { currentBet = value; } }

        public Player Player { get { return player; } set { player = value; } }

        public Move NextMove { get { return nextMove; } set { nextMove = value; } }

        public int ChairNum { get { return chairNum; } set { chairNum = value; } }
        public bool WantToExit { get { return wantToExit; } set { wantToExit = value; } }

        public Hand Hand { get { return hand; } set { hand = value; } }

        public bool IsFold { get => isFold; set => isFold = value; }

        public String GetUsername()
        {
            return player.Username;
        }


        public override string ToString()
        {
            return player.Username;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is GamePlayer)) return false;
            return this.player.Equals(((GamePlayer)obj).player);
        }
    }
}
