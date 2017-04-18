using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.Center
{
    
    class GameCenter
    {
        private List<League> leagues;
        
        public GameCenter(List<League> leagues)
        {
            this.leagues = leagues;
        }
        public List<IGame> getAllInActiveGames()
        {
            List<IGame> games = new List<IGame>();
            foreach (League l in leagues)
            {
                foreach(Room r in l.Rooms)
                {
                    games.AddRange(r.PastGames);
                }
            }
            return games;
        }
        public void replayGame(IGame game)
        {
            //will be modified in the future after adding UI.
            game.replayGame();
        }
    }
}
