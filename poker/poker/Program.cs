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
            ILeaguesData leaguesData = new LeaguesByList();
            IPlayersData playersData = new PlayersByDB(db);
            IRoomData roomsData = new RoomByList();

            // create Lagues
            League league1 = new League(100, "Level One");
            League league2 = new League(200, "Level Two");
            leaguesData.AddLeague(league1);
            leaguesData.AddLeague(league2);

            // create Users - no nedded all in DB
            //Player user1 = new Player(100, "Eliran", "1234", "eliran@gmail.com", league1);
            //Player user2 = new Player(200, "Oleg", "1234", "oleg@gmail.com", league1);
            //Player user3 = new Player(300, "Moshe", "1234", "moshe@gmail.com", league1);
            //Player user4 = new Player(400, "Slava", "1234", "slave@gmail.com", league2);
            //playersData.AddPlayer(user1);
            //playersData.AddPlayer(user2);
            //playersData.AddPlayer(user3);
            //playersData.AddPlayer(user4);

            // create rooms
            GamePreferences gp1 = new GamePreferences(GameTypePolicy.NO_LIMIT, 8, 2, 100, 1000, true, 10);
            GamePreferences gp2 = new GamePreferences(GameTypePolicy.NO_LIMIT, 6, 2, 300, 3000, true, 10);
            Room room1 = new Room(new TexasGame(gp1));
            Room room2 = new Room(new TexasGame(gp1));
            Room room3 = new Room(new TexasGame(gp2));
            Room room4 = new Room(new TexasGame(gp1));
            roomsData.AddRoom(room1);
            roomsData.AddRoom(room2);
            roomsData.AddRoom(room3);
            roomsData.AddRoom(room4);
            league1.AddRoom(room1);
            league1.AddRoom(room2);
            league1.AddRoom(room3);
            league2.AddRoom(room4);

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
