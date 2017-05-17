using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.PokerGame.Moves
{
    public class Call : Move
    {
        public Call(int amount, GamePlayer player) : base("Call", amount, player) { }


        public override Move DoAction()
        {
            return player.Call(this);
        }
    }
}
