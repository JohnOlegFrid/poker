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

        public GamePlayer(Player player, int chairNum, int money, bool isFold, Move nextMove, int currentBet, Hand hand)
        {
            this.player = player;
            this.chairNum = chairNum;
            this.money = money;
            this.isFold = isFold;
            this.nextMove = nextMove;
            this.currentBet = currentBet;
            this.hand = hand;
        }

        public int Money { get { return money; } set { money = value; } }

        public int CurrentBet { get { return currentBet; } set { currentBet = value; } }

        public Player Player { get { return player; } set { player = value; } }

        public Move NextMove { get { return nextMove; } set { nextMove = value; } }

        public int ChairNum { get { return chairNum; } set { chairNum = value; } }

        public Hand Hand { get { return hand; } set { hand = value; } }

        public String GetUsername()
        {
            return player.Username;
        }

        public bool IsFold()
        {
            return isFold;
        }

        public override string ToString()
        {
            return player.Username;
        }
    }
}
