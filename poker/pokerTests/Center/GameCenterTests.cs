﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using poker.Center;
using poker.Data;
using poker.Players;
using poker.PokerGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.Center.Tests
{
    [TestClass()]
    public class GameCenterTests
    {

        private ILeaguesData leaguesData;
        private IPlayersData usersData;
        private GameCenter gameCenter;
        private Player user1, user2, user3, user4;
        private League league1, league2, league3;

        [TestInitialize()]
        public void Initialize()
        {
            leaguesData = new LeaguesByList();
            usersData = new PlayersByList();


            // create Lagues
            league1 = new League(1, "Level One");
            league2 = new League(2, "Level Two");
            league3 = new League(1, "Level Three");
            leaguesData.AddLeague(league1);
            leaguesData.AddLeague(league2);
            leaguesData.AddLeague(league3);

            // create Users
            user1 = new Player(1, "eliran", "1234", "eliran@gmail.com", league1)
            {
                Rank = 3
            };
            user2 = new Player(2, "oleg", "1234", "oleg@gmail.com", league1)
            {
                Rank = 7
            };
            user3 = new Player(3, "moshe", "1234", "moshe@gmail.com", league2)
            {
                Rank = 4
            };
            user4 = new Player(3, "slava", "1234", "slave@gmail.com", league3);
            usersData.AddPlayer(user1);
            usersData.AddPlayer(user2);
            usersData.AddPlayer(user3);
            usersData.AddPlayer(user4);
            Room r = new Room(new TexasGame(new GamePreferences(4, 2, 100, 1000, true, 100)));
            league1.Rooms.Add(r);
            r = new Room(new TexasGame(new GamePreferences(5, 2, 200, 1000, true, 100)));
            league1.Rooms.Add(r);
            gameCenter = new GameCenter(leaguesData.GetAllLeagues(), user1);
        }

        [TestMethod()]
        public void GetAllFinishedGamesTest()
        {
            List<IGame> inActiveGames = gameCenter.GetAllFinishedGames();
            Assert.AreEqual(0, inActiveGames.Count);
            leaguesData.GetAllLeagues().ElementAt(0).Rooms.ElementAt(0).Game.StartGame();
            leaguesData.GetAllLeagues().ElementAt(0).Rooms.ElementAt(0).Game = null;
            inActiveGames = gameCenter.GetAllFinishedGames();
            Assert.AreEqual(1, inActiveGames.Count);
        }

        [TestMethod()]
        public void ReplayGameTest()
        {
            List<IGame> inActiveGames = gameCenter.GetAllFinishedGames();
            Assert.AreEqual(0, inActiveGames.Count);
            Assert.IsTrue("game is not finished, can't replay".Equals(gameCenter.ReplayGame(leaguesData.GetAllLeagues().ElementAt(0).Rooms.ElementAt(0).Game)));
            leaguesData.GetAllLeagues().ElementAt(0).Rooms.ElementAt(0).Game.StartGame();
            Assert.AreEqual("game is not finished, can't replay", gameCenter.ReplayGame(leaguesData.GetAllLeagues().ElementAt(0).Rooms.ElementAt(0).Game));
            leaguesData.GetAllLeagues().ElementAt(0).Rooms.ElementAt(0).Game = null;
            inActiveGames = gameCenter.GetAllFinishedGames();
            Assert.AreNotEqual("game is not finished, can't replay", gameCenter.ReplayGame(inActiveGames.ElementAt(0)));
        }

        [TestMethod()]
        public void MovePlayerToLeaugeTest()
        {
            Player higgestRankPlayer = gameCenter.GetHiggestRankPlayer();
            Assert.IsTrue(higgestRankPlayer.Username.Equals("oleg"));
            gameCenter.MovePlayerToLeauge(user3, league1); // not need to work ,oleg is not logged
            Assert.IsFalse(user3.League.Equals(league1));
            gameCenter.LoggedPlayer = higgestRankPlayer;
            gameCenter.MovePlayerToLeauge(user3, league1); // need to work
            Assert.IsTrue(user3.League.Equals(league1));
        }


        [TestMethod()]
        public void SetDefaultLeaguesTest()
        {
            Player higgestRankPlayer = gameCenter.GetHiggestRankPlayer();
            Assert.IsTrue(higgestRankPlayer.Username.Equals("oleg"));
            gameCenter.SetDefaultLeagues(league2); // not need to work ,oleg is not logged
            Assert.AreNotEqual(league2, gameCenter.GetDefaultLeagues());
            Assert.AreEqual(league1, gameCenter.GetDefaultLeagues()); // If not set , default will be the first league
            gameCenter.LoggedPlayer = higgestRankPlayer;
            gameCenter.SetDefaultLeagues(league2); // need to work ,oleg is logged
            Assert.AreEqual(league2, gameCenter.GetDefaultLeagues());
        }

        [TestMethod()]
        public void GetGamesAvailableToSpectateTest()
        {
            List<IGame> spectateActiveGames = gameCenter.GetGamesAvailableToSpectate();
            Assert.AreEqual(0, spectateActiveGames.Count);
            leaguesData.GetAllLeagues().ElementAt(0).Rooms.ElementAt(0).Game.StartGame();
            spectateActiveGames = gameCenter.GetGamesAvailableToSpectate();
            Assert.AreEqual(1, spectateActiveGames.Count);
            leaguesData.GetAllLeagues().ElementAt(0).Rooms.ElementAt(1).Game = new TexasGame(new GamePreferences(4, 2, 100, 1000, false, 10));
            leaguesData.GetAllLeagues().ElementAt(0).Rooms.ElementAt(1).Game.StartGame();
            spectateActiveGames = gameCenter.GetGamesAvailableToSpectate();
            Assert.AreEqual(1, spectateActiveGames.Count);
            leaguesData.GetAllLeagues().ElementAt(0).Rooms.ElementAt(0).Game = null;
            spectateActiveGames = gameCenter.GetGamesAvailableToSpectate();
            Assert.AreEqual(0, spectateActiveGames.Count);
        }
    }
}