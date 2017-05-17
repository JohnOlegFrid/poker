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

        public Move Play()
        {
            try
            {
                return nextMove.DoAction();
            }
            catch (PokerExceptions pe)
            {
                return null;
            }
        }

        public Move Call(Move call)
        {
            int balance = money - call.Amount;
            if (balance < 0)
                throw new NotEnoughMoneyException("You need more " + (balance * -1));
            this.money -= call.Amount;
            this.currentBet += call.Amount;
            return call;
        }

        //Raise amount = call amount + more amount
        public Move Raise(Raise raise)
        {
            return Call(raise);
        }

        public Move Fold(Fold fold)
        {
            this.isFold = true;
            return fold;
        }

        public void CancelMove(Move currentMove)
        {
            this.money += currentMove.Amount;
            this.currentBet -= currentMove.Amount;
        }

        public override string ToString()
        {
            return player.Username;
        }
    }
}
