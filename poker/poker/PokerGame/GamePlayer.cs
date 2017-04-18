using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Players;

namespace poker.PokerGame
{
    public class GamePlayer
    {
        private Player player;
        private int money;

        public GamePlayer(Player player, int money)
        {
            this.player = player;
            this.money = money;
        }

        public int Money { get { return money; } set { money = value; } }
    }
}
