using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.PokerGame.Moves
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class Move
    {
        [JsonProperty]
        private String name;
        [JsonProperty]
        private int amount;
        protected GamePlayer player;

        public Move(String name, int amount, GamePlayer player)
        {
            this.name = name;
            this.amount = amount;
            this.player = player;
        }

        public string Name { get { return name; } set { name = value; }}
        public int Amount { get { return amount; } set { amount = value; } }

        public GamePlayer Player { get { return player; } set { player = value; } }

        public abstract Move DoAction();

        public override String ToString()
        {
            String s =  "Player " + player.GetUsername() + " is " + name;
            if (amount > 0)
                s += " By " + amount;
            return s;
        }
    }
}
