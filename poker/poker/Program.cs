using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Data;
using poker.Players;
using poker.Center;

namespace poker
{
    class Program
    {
        static void Main(string[] args)
        {
            ILeaguesData leaguesDate = new LeaguesByList();
            IPlayersData usersData = new PlayersByList();
            InitData(leaguesDate, usersData);
            GameCenter gameCenter = new GameCenter(leaguesDate.GetAllLeagues());           

            Console.ReadKey();
        }

        private static void InitData(ILeaguesData leaguesDate, IPlayersData usersData)
        {
            // create Lagues
            League league1 = new League(1, "Level One");
            League league2 = new League(2, "Level Two");
            League league3 = new League(1, "Level Three");
            leaguesDate.AddLeague(league1);
            leaguesDate.AddLeague(league2);
            leaguesDate.AddLeague(league3);

            // create Users
            Player user1 = new Player(1, "eliran", "1234", "eliran@gmail.com", league1);
            Player user2 = new Player(2, "oleg", "1234", "oleg@gmail.com", league1);
            Player user3 = new Player(3, "moshe", "1234", "moshe@gmail.com", league2);
            Player user4 = new Player(3, "slava", "1234", "slave@gmail.com", league3);
            usersData.AddPlayer(user1);
            usersData.AddPlayer(user2);
            usersData.AddPlayer(user3);
            usersData.AddPlayer(user4);
            
        }
    }
}
