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

        public List<Room> DisplayAvailablePokerGames(Player loggedPlayer = null)
        {
            if (loggedPlayer == null) //optional argument
                loggedPlayer = this.loggedPlayer;
            return loggedPlayer.League.GetAllActiveGames();
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
            if (!game.IsActive())
                foreach (string s in game.ReplayGame())
                {
                    ans = ans + s + "/n";
                }
            else
                ans = "game is not finished, can't replay";
            return ans;
        }

        public void MovePlayerToLeauge(Player player, League league, Player loggedPlayer = null)
        {
            if (loggedPlayer == null) //optional argument
                loggedPlayer = this.loggedPlayer;
            if (loggedPlayer != GetHiggestRankPlayer())
                return;
            LeagueManager.MovePlayerToLeauge(player, league);
        }

        public Player GetHiggestRankPlayer()
        {
            Player bestPlayer = null, currentPlayer;
            int bestPlayerRank = -1;
            foreach(League league in leagues)
            {
                currentPlayer = league.getHiggestRankPlayer();
                if(currentPlayer.Rank > bestPlayerRank)
                {
                    bestPlayer = currentPlayer;
                    bestPlayerRank = bestPlayer.Rank;
                }
            }
            return bestPlayer;
        }
    }
}
