using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Players;
using poker.PokerGame.Moves;

namespace poker.PokerGame
{
    public class GamePlayer
    {
        private Player player;
        private int chairNum;
        private int money;
        private bool isFold;
        private Move nextMove;

        public GamePlayer(Player player, int money)
        {
            this.player = player;
            this.money = money;
            isFold = false;
        }

        public int Money { get { return money; } set { money = value; } }

        public Player Player { get { return player; } set { player = value; } }

        public Move NextMove { get { return nextMove; } set { nextMove = value; } }

        public int ChairNum { get { return chairNum; } set { chairNum = value; } }

        public bool IsFold()
        {
            return isFold;
        }

        public Move Play()
        {
            return nextMove.DoAction(); 
        }

        public String GetUsername()
        {
            return player.Username;
        }

    }
}
