using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.Center
{
    
    public class GameCenter
    {
        private List<League> leagues;
        
        public GameCenter(List<League> leagues)
        {
            this.leagues = leagues;
        }
        public List<IGame> getAllFinishedGames()
        {
            List<IGame> games = new List<IGame>();
            foreach (League l in leagues)
            {
                foreach(Room r in l.Rooms)
                {
                    foreach(IGame g in r.PastGames)
                        games.Add(g);
                }
            }
            return games;
        }
        public string replayGame(IGame game)
        {
            //will be modified in the future after adding UI.
            string ans = "";
            if (game.isFinished())
                foreach (string s in game.replayGame())
                {
                    ans = ans + s + "/n";
                }
            else
                ans = "game is not finished, can't replay";
            return ans;
        }
    }
}
