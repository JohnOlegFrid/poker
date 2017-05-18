using poker.PokerGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerClient.Center
{
    public interface IGame
    {
        GamePlayer[] GetChairsInGame();
        void SetChairsInGame(GamePlayer[] chairs);
    }
}
