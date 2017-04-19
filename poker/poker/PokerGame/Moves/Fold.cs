using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.PokerGame.Moves
{
    public class Fold : Move
    {
        public Fold(string name, GamePlayer player) : base("Fold", 0, player) { }

        public override Move DoAction()
        {
            return player.Fold(this);
        }
    }
}
