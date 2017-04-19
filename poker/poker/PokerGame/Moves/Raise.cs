using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.PokerGame.Moves
{
    public class Raise : Move
    {
        public Raise(string name, int amount, GamePlayer player) : base("Raise", amount, player) { }

        public override Move DoAction()
        {
            return player.Raise(this);
        }
    }
}
