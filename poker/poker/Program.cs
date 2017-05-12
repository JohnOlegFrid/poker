﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Data;
using poker.Players;
using poker.Center;
using System.IO;
using poker.Server;

namespace poker
{
    public class Program
    {

        public static void Main(string[] args)
        {
            Console.WriteLine("Multi-Threaded TCP Server Starting..");
            TcpServer server = new TcpServer(5555);
        }

        static void Main1(string[] args)
        {
            ILeaguesData leaguesData = new LeaguesByList();
            IPlayersData usersData = new PlayersByList();
            InitData(leaguesData, usersData);
            SearchCenter searchCenter = new SearchCenter(usersData, leaguesData);
            Player playerLogged = new Player(5, "logged", "1234", "logged@gmail.com", leaguesData.GetDefalutLeague());
            usersData.AddPlayer(playerLogged);
            GameCenter gameCenter = new GameCenter(leaguesData.GetAllLeagues(), playerLogged);

            Console.ReadKey();
        }

        public static void InitData(ILeaguesData leaguesData, IPlayersData usersData)
        {
            // create Lagues
            League league1 = new League(1, "Level One");
            League league2 = new League(2, "Level Two");
            League league3 = new League(1, "Level Three");
            leaguesData.AddLeague(league1);
            leaguesData.AddLeague(league2);
            leaguesData.AddLeague(league3);

            // create Users
            Player user1 = new Player(1, "eliran", "1234", "eliran@gmail.com", league1);
            Player user2 = new Player(2, "oleg", "1234", "oleg@gmail.com", league1);
            Player user3 = new Player(3, "moshe", "1234", "moshe@gmail.com", league2);
            Player user4 = new Player(4, "slava", "1234", "slave@gmail.com", league3);
            usersData.AddPlayer(user1);
            usersData.AddPlayer(user2);
            usersData.AddPlayer(user3);
            usersData.AddPlayer(user4);

        }
    }
}
