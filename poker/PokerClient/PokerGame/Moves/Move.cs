using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.PokerGame.Moves
{
    public class Move
    {
        private String name;
        private int amount;
        //protected GamePlayer player;

        public Move(String name, int amount)
        {
            this.name = name;
            this.amount = amount;

        }

        public string Name { get { return name; } set { name = value; }}
        public int Amount { get { return amount; } set { amount = value; } }

       // public GamePlayer Player { get { return player; } set { player = value; } }

        public override String ToString()
        {
            String s =  "Player " + " is " + name;
            if (amount > 0)
                s += " By " + amount;
            return s;
        }
    }
}
