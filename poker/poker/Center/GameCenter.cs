using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Players;

namespace poker.Center
{
    public class GameCenter
    {
        private List<League> leagues;
        private Player loggedPlayer;
        private League defaultLeagues = null;
        
        public GameCenter(List<League> leagues, Player loggedPlayer)
        {
            this.leagues = leagues;
            this.loggedPlayer = loggedPlayer;
        }
        public Player LoggedPlayer
        {
            get { return loggedPlayer; }
            set { loggedPlayer = value; }
        }


        public List<IGame> GetAllFinishedGames()
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
        public string ReplayGame(IGame game)
        {
            //will be modified in the future after adding UI.
            string ans = "";
            if (GetAllFinishedGames().Contains(game))
                foreach (string s in game.ReplayGame())
                {
                    ans = ans + s + "/n";
                }
            else
                ans = "game is not finished, can't replay";
            return ans;
        }



        public League GetDefaultLeagues()
        {
            if (this.defaultLeagues != null)
                return defaultLeagues;
            return leagues[0];
        }



        public List<IGame> GetGamesAvailableToSpectate()
        {
            List<IGame> games = new List<IGame>();
            foreach (League l in leagues)
            {
                foreach (Room r in l.Rooms)
                {
                    if (r.Game!=null && r.Game.IsActive() && r.Game.IsAllowSpectating())
                        games.Add(r.Game);
                }
            }
            return games;
        }
    }
}
