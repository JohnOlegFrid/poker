using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using poker.PokerGame;
using poker.Data;
using poker.ServiceLayer;
using pokerTests;

namespace poker.Center.Tests
{
    [TestClass()]
    public class LeagueTests
    {
        [TestMethod()]
        public void AddLeagueTest()
        {
            ProgramList.InitData();
            League leauge = new League(100, "leauge");
            Program.leaguesData.AddLeague(leauge);
            Assert.IsNotNull(Program.leaguesData.FindLeagueById(100));
        }

        [TestMethod()]
        public void RemoveLeagueTest()
        {
            ProgramList.InitData();
            League leauge = new League(100, "leauge");
            Program.leaguesData.AddLeague(leauge);
            Program.leaguesData.DeleteLeague(leauge);
            Assert.IsNull(Program.leaguesData.FindLeagueById(100));
        }

        [TestMethod()]
        public void AddRoomToLeagueTest()
        {
            ProgramList.InitData();
            League leauge = new League(100, "leauge");
            Room room = new Room(100, new TexasGame(new GamePreferences(GamePreferences.GameTypePolicy.LIMIT, 5, 2, 1, 2, true, 10)));
            Program.leaguesData.AddRoomToLeague(leauge, room);
            Assert.AreEqual(1, leauge.Rooms.Count);
        }

        [TestMethod()]
        public void RemoveRoomFromLeagueTest()
        {
            ProgramList.InitData();
            League leauge = new League(100, "leauge");
            Room room = new Room(100, new TexasGame(new GamePreferences(GamePreferences.GameTypePolicy.LIMIT, 5, 2, 1, 2, true, 10)));
            Program.leaguesData.AddRoomToLeague(leauge, room);
            Program.leaguesData.RemoveRoomFromLeague(leauge, room);
            Assert.AreEqual(0, leauge.Rooms.Count);
        }

    }
}