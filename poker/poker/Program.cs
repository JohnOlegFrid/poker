using System;
using poker.Data;
using poker.Players;
using poker.Center;
using poker.Server;
using poker.ServiceLayer;
using poker.PokerGame;
using System.Net;
using System.Net.Sockets;
using static poker.PokerGame.GamePreferences;
using poker.Data.DB;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace poker
{
    public class Program
    {
        // use this to call function on exit console
        static ConsoleEventDelegate handler;   // Keeps it from getting garbage collected
        private delegate bool ConsoleEventDelegate(int eventType);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleCtrlHandler(ConsoleEventDelegate callback, bool add);
        //
        private static DBConnection db;

        public static void Main(string[] args)
        {
            handler = new ConsoleEventDelegate(ConsoleEventCallback);
            SetConsoleCtrlHandler(handler, true);
            Console.WriteLine("Loading Data Please Wait...");
            InitData();
            Console.WriteLine("Multi-Threaded TCP Server Starting On IP:" + GetLocalIPAddress());
            TcpServer server = new TcpServer(5555);
        }

        static bool ConsoleEventCallback(int eventType)
        {
            if (eventType == 2) // console window closings
            {
                db.Close();
            }
            return false;
        }

        public static void InitData()
        {
            db = InitDB();
            ILeaguesData leaguesData = new LeaguesByDB(db);
            IPlayersData playersData = new PlayersByDB(db);
            IRoomData roomsData = new RoomsByDB(db);

            // create Lagues - no nedded all in DB
            //League league1 = new League(100, "Level One");
            League league1 = leaguesData.FindLeagueById(100);
            League league2 = leaguesData.FindLeagueById(200);
            //leaguesData.AddLeague(league1);
            //leaguesData.AddLeague(league2);

            // create Users - no nedded all in DB
            //Player user1 = new Player(100, "Eliran", "1234", "eliran@gmail.com", league1);
            //Player user2 = new Player(200, "Oleg", "1234", "oleg@gmail.com", league1);
            //Player user3 = new Player(300, "Moshe", "1234", "moshe@gmail.com", league1);
            //Player user4 = new Player(400, "Slava", "1234", "slave@gmail.com", league2);
            //playersData.AddPlayer(user1);
            //playersData.AddPlayer(user2);
            //playersData.AddPlayer(user3);
            //playersData.AddPlayer(user4);

            Player player1 = playersData.FindPlayerByUsername("Eliran");

            // create rooms
            //GamePreferences gp1 = new GamePreferences(GameTypePolicy.NO_LIMIT, 8, 2, 100, 1000, true, 10);
            //GamePreferences gp2 = new GamePreferences(GameTypePolicy.NO_LIMIT, 6, 2, 300, 3000, true, 10);
            //Room room1 = new Room(1, new TexasGame(gp1));
            Room room1 = roomsData.FindRoomById(1);
            //Room room2 = new Room(2, new TexasGame(gp1));
            Room room2 = roomsData.FindRoomById(2);
            //Room room3 = new Room(3, new TexasGame(gp2));
            Room room3 = roomsData.FindRoomById(3);
            //Room room4 = new Room(4, new TexasGame(gp1));
            Room room4 = roomsData.FindRoomById(4);
            //roomsData.AddRoom(room1);
            //roomsData.AddRoom(room2);
            //roomsData.AddRoom(room3);
            //roomsData.AddRoom(room4);
            //leaguesData.AddRoomToLeague(room1, league1);
            //leaguesData.AddRoomToLeague(room2, league1);
            //leaguesData.AddRoomToLeague(room3, league1);
            //leaguesData.AddRoomToLeague(room4, league2);
  
            //create service layer
            new Service(leaguesData, roomsData, playersData);
        }

        private static DBConnection InitDB()
        {
            DBConnection db = new DBConnection("sql8.freemysqlhosting.net", "sql8176777", "sql8176777", "k8uzaBcq64");
            db.Connect();
            return db;
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }
    }
}
