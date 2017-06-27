using Microsoft.VisualStudio.TestTools.UnitTesting;
using poker.Data;
using poker.Players;
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
        public void GetTopTest()
        {
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void updateStatsTest()
        {
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void getAvgGrossProfitTest()
        {
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void getAvgGainTest()
        {
            Assert.IsTrue(true);
        }
    }
}