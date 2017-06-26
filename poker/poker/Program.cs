using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using poker.Data;
using poker.Players;
using poker.Center;
using poker.Server;
using poker.ServiceLayer;
using poker.PokerGame;
using System.Net;
using System.Net.Sockets;
using static poker.PokerGame.GamePreferences;
using poker.Security;
using poker.Data.DB;
using poker.Logs;

namespace poker
{
    public class Program
    {
        public static ILeaguesData leaguesData;
        public static IPlayersData playersData;
        public static IRoomData roomsData;
        public static string key = "fvggtzYH675PiXpjK5fGuGhadAa5Sjb1G4hUQobzlls=";
        public static string iv = "2EPvpwkqNxcc4qmKlPv80cpNWuVu6ypjwhGGE5dceMI=";
        public static myDB db = new myDB();

        public static void Main(string[] args)
        {
            if (!ValidateConnectioToSqlServer())
            {
                Log.ErrorLog("Error on conneting to sql server");
                Console.WriteLine("Error on conneting to sql server, please try again!");
                Console.WriteLine("Prees any key to exit.");
                Console.ReadKey();
                return;
            }   
            
            InitData();

            //try to restore backup if there are a crash
            RecoveryGame.RestoreBackupGames();

            Console.WriteLine("Multi-Threaded TCP Server Starting On IP:" + GetLocalIPAddress() + "...");
            Log.InfoLog("Multi-Threaded TCP Server Starting On IP:" + GetLocalIPAddress() + "...");
            TcpServer server = new TcpServer(5555);
        }

        private static bool ValidateConnectioToSqlServer()
        {
            return db.Database.Exists();
        }

        public static void InitData()
        {            
            leaguesData = new LeaguesByDB();
            playersData = new PlayersByDB();
            roomsData = new RoomsByDB();
            Service service = new Service(leaguesData, roomsData, playersData);
            Log.InfoLog("Service Layer initiated");
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
