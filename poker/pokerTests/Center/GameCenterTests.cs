using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        [TestInitialize()]
        public void Initialize()
        {
            leaguesData = new LeaguesByList();
            usersData = new PlayersByList();
            

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
            Player user4 = new Player(3, "slava", "1234", "slave@gmail.com", league3);
            usersData.AddPlayer(user1);
            usersData.AddPlayer(user2);
            usersData.AddPlayer(user3);
            usersData.AddPlayer(user4);
            
            Room r = new Room(new TexasGame(new GamePreferences(4, false, 100, 1000)));
            league1.Rooms.Add(r);
            r = new Room(new TexasGame(new GamePreferences(5, false, 200, 1000)));
            league1.Rooms.Add(r);
            gameCenter = new GameCenter(leaguesData.GetAllLeagues());
        }

        [TestCleanup()]
        public void Cleanup()
        {
            leaguesData = new LeaguesByList();
            usersData = new PlayersByList();


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
            Player user4 = new Player(3, "slava", "1234", "slave@gmail.com", league3);
            usersData.AddPlayer(user1);
            usersData.AddPlayer(user2);
            usersData.AddPlayer(user3);
            usersData.AddPlayer(user4);
            Room r = new Room(new TexasGame(new GamePreferences(4, false, 100, 1000)));
            league1.Rooms.Add(r);
            r = new Room(new TexasGame(new GamePreferences(5, false, 200, 1000)));
            league1.Rooms.Add(r);
            gameCenter = new GameCenter(leaguesData.GetAllLeagues());
        }

        [TestMethod()]
        public void getAllFinishedGamesTest()
        {
            List<IGame> inActiveGames = gameCenter.getAllFinishedGames();
            Assert.AreEqual(0, inActiveGames.Count);
            leaguesData.GetAllLeagues().ElementAt(0).Rooms.ElementAt(0).Game.startGame();
            leaguesData.GetAllLeagues().ElementAt(0).Rooms.ElementAt(0).Game=null;
            inActiveGames = gameCenter.getAllFinishedGames();
            Assert.AreEqual(1, inActiveGames.Count);
        }

        [TestMethod()]
        public void replayGameTest()
        {
            List<IGame> inActiveGames = gameCenter.getAllFinishedGames();
            Assert.AreEqual(0, inActiveGames.Count);
            Assert.AreEqual("game is not finished, can't replay", gameCenter.replayGame(leaguesData.GetAllLeagues().ElementAt(0).Rooms.ElementAt(0).Game));
            leaguesData.GetAllLeagues().ElementAt(0).Rooms.ElementAt(0).Game.startGame();
            Assert.AreEqual("game is not finished, can't replay", gameCenter.replayGame(leaguesData.GetAllLeagues().ElementAt(0).Rooms.ElementAt(0).Game));
            leaguesData.GetAllLeagues().ElementAt(0).Rooms.ElementAt(0).Game = null;
            inActiveGames = gameCenter.getAllFinishedGames();
            Assert.AreNotEqual("game is not finished, can't replay",gameCenter.replayGame(inActiveGames.ElementAt(0)));
        }
    }
}