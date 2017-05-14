using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using poker.Data;
using poker.Players;
using poker.Center;
using poker.Server;
using poker.ServiceLayer;

namespace poker
{
    public class Program
    {

        public static void Main(string[] args)
        {
            InitData();
            Console.WriteLine("Multi-Threaded TCP Server Starting..");
            TcpServer server = new TcpServer(5555);
        }

        public static void InitData()
        {
            ILeaguesData leaguesData = new LeaguesByList();
            IPlayersData playersData = new PlayersByList();
            IRoomData roomsData = new RoomByList();

            // create Lagues
            League league1 = new League(100, "Level One");
            League league2 = new League(200, "Level Two");
            League league3 = new League(300, "Level Three");
            leaguesData.AddLeague(league1);
            leaguesData.AddLeague(league2);
            leaguesData.AddLeague(league3);

            // create Users
            Player user1 = new Player(100, "Eliran", "1234", "eliran@gmail.com", league1);
            Player user2 = new Player(200, "Oleg", "1234", "oleg@gmail.com", league1);
            Player user3 = new Player(300, "Moshe", "1234", "moshe@gmail.com", league2);
            Player user4 = new Player(400, "Slava", "1234", "slave@gmail.com", league3);
            playersData.AddPlayer(user1);
            playersData.AddPlayer(user2);
            playersData.AddPlayer(user3);
            playersData.AddPlayer(user4);

            //create service layer
            new Service(leaguesData, roomsData, playersData);
        }
    }
}
