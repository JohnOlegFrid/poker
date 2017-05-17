using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.PokerGame.Moves
{
    public class Check : Move
    {
        public Check(GamePlayer player) : base("Check", 0, player) { }

        public override Move DoAction()
        {
            return this;
        }
    }
}
