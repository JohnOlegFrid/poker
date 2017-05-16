using Microsoft.VisualStudio.TestTools.UnitTesting;
using poker.Center;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Players;
using poker.PokerGame;
using poker.Data;
using poker.ServiceLayer;

namespace poker.Center.Tests
{
    [TestClass()]
    public class LeagueTests
    {

        [TestMethod()]
        public void DisplayAvailablePokerGamesTest()
        {
            Program.InitData();
            ILeaguesData leaguesData = Service.GetLastInstance().LeaguesData;
            GameCenter gameCenter = new GameCenter(Service.GetLastInstance().LeaguesData.GetAllLeagues(),
                Service.GetLastInstance().PlayersData.FindPlayerByUsername("Eliran"));
            League league = leaguesData.GetDefalutLeague();
            GamePreferences gp = new GamePreferences(4, 2, 100, 1000, true, 100);
            Room room1 = new Room(new TexasGame(gp));
            league.AddRoom(room1);
            room1.HaveActiveGame = true;
            Room room2 = new Room(new TexasGame(gp))
            {
                HaveActiveGame = false
            };
            league.AddRoom(room2);
            List<Room> activeGames = new List<Room> { room1 };
            // used emmpty GetAllActiveGame that use his field
            List<Room> listEmptyArgs = gameCenter.DisplayAvailablePokerGames();
            Assert.IsFalse(Enumerable.SequenceEqual(listEmptyArgs, activeGames));
            List<Room> listWithArgs = gameCenter.DisplayAvailablePokerGames(gameCenter.LoggedPlayer);
            Assert.IsFalse(Enumerable.SequenceEqual(listWithArgs, activeGames));
        }
    }
}