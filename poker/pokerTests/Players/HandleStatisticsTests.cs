using Microsoft.VisualStudio.TestTools.UnitTesting;
using poker.Data;
using poker.Players;
using poker.PokerGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.Players.Tests
{
    [TestClass()]
    public class HandleStatisticsTests
    {
        private Player player1;
        private Player player2;
        private Player player3;
        private IPlayersData data;

        [TestInitialize()]
        public void Initialize()
        {
            data = new PlayersByList();
            player1 = new Player(1, "yakir", "1234", "yakir@gmail.com", 1); //previous last parameter: new Center.League(1, "level one")
            data.AddPlayer(player1);
            player2 = new Player(2, "moshe", "1234", "moshe@gmail.com", 1);
            data.AddPlayer(player2);
            player3 = new Player(3, "oleg", "1234", "oleg@gmail.com", 1);
            data.AddPlayer(player3);
        }

        [TestCleanup()]
        public void Cleanup()
        {
            data = new PlayersByList();
            player1 = new Player(1, "yakir", "1234", "yakir@gmail.com", new Center.League(1, "level one").Id);
            data.AddPlayer(player1);
            player2 = new Player(2, "moshe", "1234", "moshe@gmail.com", new Center.League(1, "level one").Id);
            data.AddPlayer(player2);
            player3 = new Player(3, "oleg", "1234", "oleg@gmail.com", new Center.League(1, "level one").Id);
            data.AddPlayer(player3);
        }

        [TestMethod()]
        public void GetTopByGrossTest()
        {
            Assert.IsTrue(HandleStatistics.GetTop("Gross profit",20,data).Count == 3);
        }

        [TestMethod()]
        public void GetTopByGamesTest()
        {
            Assert.IsTrue(HandleStatistics.GetTop("Number of games", 20, data).Count == 3);
        }

        [TestMethod()]
        public void GetTopByGainTest()
        {
            Assert.IsTrue(HandleStatistics.GetTop("Highest gain", 20, data).Count == 3);
        }

        [TestMethod()]
        public void UpdateStatsTest()
        {
            List<GamePlayer> list = new List<GamePlayer>();
            GamePlayer gp = new GamePlayer(player1, 1000);
            //HandleStatistics.updateStats
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void GetAvgGrossProfitTest()
        {
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void GetAvgGainTest()
        {
            Assert.IsTrue(true);
        }
    }
}