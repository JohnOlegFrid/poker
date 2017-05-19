using System;
using poker.Players;
using poker.PokerGame.Moves;
using poker.PokerGame.Exceptions;
using poker.Cards;

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

        public GamePlayer(Player player, int money)
        {
            this.player = player;
            this.money = money;
            isFold = false;
            currentBet = 0;
            hand = new Hand();
        
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

        public override bool Equals(object obj)
        {
            return obj.ToString().Equals(this.ToString());
        }
    }
}
